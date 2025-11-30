namespace ECommerceAPI.Models.Options;

public record SeedFileOptions
{
    public string? Type { get; set; }
    public string? Path { get; set; }
    public string? Sheet { get; set; }
}