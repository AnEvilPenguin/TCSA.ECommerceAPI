namespace ECommerceAPI.Models.DTOs;

public class ProductDto
{
    public int ID { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }

    public static ProductDto FromProduct(Product product) =>
        new()
        {
            ID = product.ID,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price
        };
}