using ECommerceAPI.Models;
using ECommerceAPI.Models.Options;
using FileSignatures;
using Microsoft.Extensions.Options;

namespace ECommerceAPI.Services.Files;

public class FileSeeder(
    ILogger<FileSeeder> logger,
    IOptions<SeedDataOptions> configuration,
    IFileFormatInspector formatInspector,
    ICsvSeeder csvSeeder) : IFileSeeder
{
    private readonly SeedDataOptions _options = configuration.Value;

    // TODO CSV Service
    // TODO Excel Service

    public IEnumerable<Product> GetProducts()
    {
        var path = _options.Products?.Path;
        
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
            return csvSeeder.GetProducts(path);

        logger.LogInformation("Seeding Products");
        throw new NotImplementedException();
    }

    private FileFormat? GetFileType(string? path)
    {
        if (!File.Exists(path))
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
}