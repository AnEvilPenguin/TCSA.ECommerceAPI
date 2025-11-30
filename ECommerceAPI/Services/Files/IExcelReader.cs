using DocumentFormat.OpenXml.Spreadsheet;

namespace ECommerceAPI.Services.Files;

public interface IExcelReader
{
    public IEnumerable<T> ReadAllRows<T>(string path, string? sheetName) where T : class, new();
}