namespace ECommerceAPI.Models.DTOs;

public class ProductSaleDto
{
    public int ID { get; set; }
    public int Quantity { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    public static ProductSaleDto FromProductSale(ProductSale productSale) =>
        new()
        {
            ID = productSale.SaleID,
            Quantity = productSale.Quantity,
            FirstName = productSale.Sale.FirstName,
            LastName = productSale.Sale.LastName,
        };
}
