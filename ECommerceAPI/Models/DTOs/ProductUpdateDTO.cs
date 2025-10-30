namespace ECommerceAPI.Models.DTOs;

public class ProductUpdateDTO
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
}