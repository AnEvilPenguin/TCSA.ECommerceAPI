namespace ECommerceAPI.Models;

public class Sale
{
    public int ID { get; set; }
    public required ICollection<ProductSale> ProductSales { get; set; }
}