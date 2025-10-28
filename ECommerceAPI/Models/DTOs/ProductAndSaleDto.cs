namespace ECommerceAPI.Models.DTOs;

public class ProductAndSaleDto
{
    public int ID { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public required ProductSaleDto[] Sales { get; set; }

    public static ProductAndSaleDto FromProductAndSale(Product product) =>
        new ()
        {
            ID = product.ID,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            Sales = product.ProductSales
                ?.Select(ProductSaleDto.FromProductSale).ToArray() 
                    ?? Enumerable.Empty<ProductSaleDto>().ToArray()
        };
}