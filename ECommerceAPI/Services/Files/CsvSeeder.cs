using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using ECommerceAPI.Models;

namespace ECommerceAPI.Services.Files;

public sealed class CsvSeeder(ILogger<CsvSeeder> logger) : ICsvSeeder
{
    private CsvConfiguration _config = new(CultureInfo.InvariantCulture)
    {
        HasHeaderRecord = true,
        PrepareHeaderForMatch = args => args.Header.ToLower(),
    };

    public IEnumerable<Product> GetProducts(string path)
    {
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, _config);

        try
        {
            return csv.GetRecords<Product>().ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
        }

        return Enumerable.Empty<Product>().ToList();
    }
}