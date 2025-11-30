using ECommerceAPI.Models;
using ECommerceAPI.Models.Options;
using FileSignatures;
using Microsoft.Extensions.Options;

namespace ECommerceAPI.Services.Files;

public class FileSeeder(
    ILogger<FileSeeder> logger,
    IOptions<SeedDataOptions> configuration,
    IFileFormatInspector formatInspector,
    ICsvSeeder csvSeeder,
    IExcelSeeder excelSeeder) : IFileSeeder
{
    private readonly SeedDataOptions _options = configuration.Value;

    public IEnumerable<Product> GetProducts()
    {
        var seeder = GetSeeder(_options.Products);
        
        if (seeder == null || _options.Products == null)
            return Enumerable.Empty<Product>();
        
        logger.LogInformation("Seeding Products");
        return seeder.GetProducts(_options.Products);
    }

    public IEnumerable<Category> GetCategories()
    {
        var seeder = GetSeeder(_options.Categories);
        
        if (seeder == null || _options.Categories == null)
            return Enumerable.Empty<Category>();
        
        logger.LogInformation("Seeding Categories");
        return seeder.GetCategories(_options.Categories);
    }

    public IEnumerable<Sale> GetSales()
    {
        var seeder = GetSeeder(_options.Sales);
        
        if (seeder == null || _options.Sales == null)
            return Enumerable.Empty<Sale>();
        
        logger.LogInformation("Seeding Sales");
        return seeder.GetSales(_options.Sales);
    }

    public IEnumerable<ProductSale> GetProductSales()
    {
        var seeder = GetSeeder(_options.ProductSales);
        
        if (seeder == null || _options.ProductSales == null)
            return Enumerable.Empty<ProductSale>();
        
        logger.LogInformation("Seeding ProductSales");
        return seeder.GetProductSales(_options.ProductSales);
    }

    private FileFormat? GetFileType(string? path)
    {
        if  (string.IsNullOrWhiteSpace(path))
            return null;
        
        var fullPath = Path.GetFullPath(path);
        
        if (!File.Exists(fullPath))
            return null;

        try
        {
            using var stream = File.OpenRead(path);

            return formatInspector.DetermineFileFormat(stream);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error reading file");
        }

        return null;
    }

    private ISeeder? GetSeeder(SeedFileOptions? options)
    {
        string? path = options?.Path;
        
        if (string.IsNullOrWhiteSpace(path))
            return null;
        
        return GetSeeder(path);
    }
    
    private ISeeder? GetSeeder(string path)
    {
        // If text assume CSV for the moment and then catch explosions?
        // There might be some validation options in CSV Helper:
        // https://joshclose.github.io/CsvHelper/examples/configuration/class-maps/validation/
        var format = GetFileType(path);

        if (format == null)
        {
            logger.LogError($"File {path} not found");
            throw new FileNotFoundException($"File {path} not found");
        }

        if (format.MediaType is "text/plain" or "text/csv")
            return csvSeeder;
        if (format.MediaType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            return excelSeeder;

        return null;
    }
}