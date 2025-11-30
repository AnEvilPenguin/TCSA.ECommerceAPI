using System.Reflection;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ECommerceAPI.Services.Files;

public sealed class ExcelReader : IExcelReader
{
    private static readonly Regex ColumnRegex = new Regex("[A-Z]+", RegexOptions.Compiled);
    
    public IEnumerable<T> ReadAllRows<T>(string path, string? sheetName) where T : class , new()
    {
        using var document = SpreadsheetDocument.Open(path, false);
        var workbookPart = document.WorkbookPart;

        if (workbookPart == null)
            yield break;

        var worksheets = workbookPart.Workbook.Sheets?.Elements<Sheet>();

        var sheet = string.IsNullOrWhiteSpace(sheetName)
            ? worksheets?.FirstOrDefault()
            : worksheets?.FirstOrDefault(s => s.Name == sheetName);

        string? sheetId = sheet?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(sheetId))
            yield break;

        var worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheetId);

        // Excel normalizes strings to reduce space usage!
        var sharedStrings = workbookPart.SharedStringTablePart?.SharedStringTable;

        var rows = worksheetPart.Worksheet.Descendants<Row>().ToList();

        var headerRow = rows.First();
        var headerCells = headerRow.Elements<Cell>();
        
        var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        
        var lookup = new Dictionary<int, PropertyInfo>();

        foreach (var cell in headerCells)
        {
            var column = GetCellColumn(cell);
            var header = GetCellValue(cell, sharedStrings);
            
            var prop = props.FirstOrDefault(p => 
                string.Equals(p.Name, header, StringComparison.CurrentCultureIgnoreCase));
            
            if (prop == null)
                continue;
            
            lookup.Add(column, prop);
        }

        foreach (var row in rows.Skip(1))
            yield return MapRow<T>(row, lookup, sharedStrings);
    }

    private static T MapRow<T>(Row row, Dictionary<int, PropertyInfo> lookup, SharedStringTable? sharedStrings) where T : class, new()
    {
        var cells = row.Elements<Cell>().ToList();

        var obj = new T();

        foreach (var cell in cells)
        {
            var column = GetCellColumn(cell);
            
            if (!lookup.TryGetValue(column, out var prop))
                continue;
            
            var cellValue = GetCellValue(cell, sharedStrings);
            
            if (string.IsNullOrWhiteSpace(cellValue))
                continue;
            
            var convertedValue = prop.PropertyType.Name == "Boolean" ? 
                string.Equals(cellValue, "1") :
                Convert.ChangeType(cellValue, prop.PropertyType);
            
            prop.SetValue(obj, convertedValue);
        }
        
        return obj;
    }

    private static string GetCellValue(Cell? cell, SharedStringTable? sharedStrings)
    {
        if (cell == null)
            return string.Empty;

        var value = cell.InnerText;

        if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString && sharedStrings != null)
            return sharedStrings.ElementAt(int.Parse(value)).InnerText;

        return value;
    }

    private static int GetCellColumn(Cell cell)
    {
        var reference = cell.CellReference;
            
        if (reference == null)
            throw new Exception("Cell reference is null");
            
        var column = ColumnRegex.Match(reference!).Groups[0].Value;
        int columnIndex = 0;

        for (int i = 0; i < column.Length; i++)
            columnIndex = columnIndex + (column[i] - 65) + 26 * i;
        
        return columnIndex;
    }
}