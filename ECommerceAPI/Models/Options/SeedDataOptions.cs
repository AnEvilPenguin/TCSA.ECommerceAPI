namespace ECommerceAPI.Models.Options;

public record SeedDataOptions
{
    public bool Enabled { get; set; }
    public SeedFileOptions? Products { get; set; }
    public SeedFileOptions? Categories { get; set; }
    public SeedFileOptions? Sales { get; set; }
    public SeedFileOptions? ProductSales { get; set; }
}