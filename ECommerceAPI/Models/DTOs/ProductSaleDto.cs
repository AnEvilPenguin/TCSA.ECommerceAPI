namespace ECommerceAPI.Models.DTOs;

// FIXME This class is not necessary
// https://learn.microsoft.com/en-us/ef/core/modeling/relationships/many-to-many

public class ProductSaleDto
{
    public int ID { get; set; }
    public int ProductId { get; set; }
    public int SaleId { get; set; }
    public int Quantity { get; set; }

    public static ProductSaleDto FromProductSale(ProductSale productSale) =>
        new()
        {
            ID = productSale.ID,
            ProductId = productSale.ProductID,
            SaleId = productSale.SaleID,
            Quantity = productSale.Quantity,
        };
}