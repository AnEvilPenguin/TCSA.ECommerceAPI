namespace ECommerceAPI.Models;

// FIXME This class is not necessary
// https://learn.microsoft.com/en-us/ef/core/modeling/relationships/many-to-many

public class ProductSale
{
    public int ID { get; set; }
    public int ProductID { get; set; }
    public int SaleID { get; set; }
    public int Quantity { get; set; }
    
    public Product Product { get; set; }
    public Sale Sale { get; set; }
}