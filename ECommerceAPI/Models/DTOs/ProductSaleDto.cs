namespace ECommerceAPI.Models.DTOs;

public class ProductSaleDto
{
    public int ProductId { get; set; }
    public int SaleId { get; set; }
    public int Quantity { get; set; }

    public static ProductSaleDto FromProductSale(ProductSale productSale) =>
        new()
        {
            ProductId = productSale.ProductID,
            SaleId = productSale.SaleID,
            Quantity = productSale.Quantity,
        };
}