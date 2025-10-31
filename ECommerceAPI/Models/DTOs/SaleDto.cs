namespace ECommerceAPI.Models.DTOs;

public class SaleDto
{
    public int ID { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    public static SaleDto FromSale(Sale sale) =>
        new()
        {
            ID = sale.ID,
            FirstName = sale.FirstName,
            LastName = sale.LastName
        };
}

public class SaleUpdateDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}