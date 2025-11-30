using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using ECommerceAPI.Models;
using ECommerceAPI.Models.Options;

namespace ECommerceAPI.Services.Files;

public sealed class CsvSeeder(ILogger<CsvSeeder> logger) : ICsvSeeder
{
    private CsvConfiguration _config = new(CultureInfo.InvariantCulture)
    {
        HasHeaderRecord = true,
        PrepareHeaderForMatch = args => args.Header.ToLower(),
        HeaderValidated = null,
        MissingFieldFound = null,
    };

    public IEnumerable<Product> GetProducts(SeedFileOptions options) =>
        GetItems<Product>(options);


    public IEnumerable<Category> GetCategories(SeedFileOptions options) =>
        GetItems<Category>(options);


    public IEnumerable<Sale> GetSales(SeedFileOptions options) =>
        GetItems<Sale>(options);


    public IEnumerable<ProductSale> GetProductSales(SeedFileOptions options) =>
        GetItems<ProductSale>(options);


    private IEnumerable<T> GetItems<T>(SeedFileOptions options)
    {
        var path = options.Path;
        
        if (string.IsNullOrWhiteSpace(path))
            return Enumerable.Empty<T>();
        
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, _config);

        try
        {
            return csv.GetRecords<T>().ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
        }

        return Enumerable.Empty<T>().ToList();
    }
}