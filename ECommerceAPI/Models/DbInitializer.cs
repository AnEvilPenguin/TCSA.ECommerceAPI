namespace ECommerceAPI.Models;

public class DbInitializer
{
    public static void Initialize(CommerceContext context)
    {
        if (context.Products.Any())
        {
            return;
        }

        var products = new Product[]
        {
            new Product { Name = "Shadow Alchemist: Alchemy in the Shadows" },
            new Product { Name = "F-You Earth!", Description = "My second game jam entry" }
        };

        context.Products.AddRange(products);
        context.SaveChanges();

        var sales = new Sale[]
        {
            new Sale { FirstName = "John", LastName = "Doe" },
            new Sale { FirstName = "Jane", LastName = "Doe" },
        };
        
        context.Sales.AddRange(sales);
        context.SaveChanges();

        var productSales = new ProductSale[]
        {
            new ProductSale { Price = new decimal(3.50d), ProductID = 1, Quantity = 1, SaleID = 1 },
            new ProductSale { Price = new decimal(4.20d), ProductID = 2, Quantity = 2, SaleID = 2 },
            new ProductSale { Price = new decimal(5.00d), ProductID = 1, Quantity = 1, SaleID = 1 },
        };
        
        context.ProductSales.AddRange(productSales);
        context.SaveChanges();
    }
}