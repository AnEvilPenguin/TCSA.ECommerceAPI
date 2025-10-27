namespace ECommerceAPI.Models;

public class Sale
{
    public int ID { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public ICollection<ProductSale>? ProductSales { get; set; }
}