namespace ECommerceAPI.Models;

public class Product
{
    public int ID { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
}