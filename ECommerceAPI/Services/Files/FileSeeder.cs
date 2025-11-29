using ECommerceAPI.Models.Options;
using FileSignatures;
using Microsoft.Extensions.Options;

namespace ECommerceAPI.Services.Files;

public class FileSeeder(
    ILogger<FileSeeder> logger,
    IOptions<SeedDataOptions> configuration,
    IFileFormatInspector formatInspector) : IFileSeeder
{
    private readonly SeedDataOptions _options = configuration.Value;

    // TODO CSV Service
    // TODO Excel Service

    public void GetProducts()
    {
        var path = _options.Products?.Path;
        
        // If text assume CSV for the moment and then catch explosions?
        // There might be some validation options in CSV Helper:
        // https://joshclose.github.io/CsvHelper/examples/configuration/class-maps/validation/
        var format = GetFileType(path);

        if (format == null)
        {
            logger.LogError($"File {path} not found");
            return;
        }

        if (format.MediaType == "text/plain" || format.MediaType == "text/csv")
        {
            Console.WriteLine("CSV");
        }

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