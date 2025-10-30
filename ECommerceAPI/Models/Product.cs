using System.Text.Json.Serialization;

namespace ECommerceAPI.Models;

public class Product
{
    [JsonIgnore]
    public int ID { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    [JsonIgnore]
    public bool Deleted { get; set; }
    [JsonIgnore]
    public ICollection<ProductSale>? ProductSales { get; set; }
}