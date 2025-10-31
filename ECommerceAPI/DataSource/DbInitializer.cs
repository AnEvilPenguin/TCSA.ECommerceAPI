using ECommerceAPI.Models;

namespace ECommerceAPI.DataSource;

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
            new() { Name = "Shadow Alchemist: Alchemy in the Shadows", Price = new decimal(3.50d) },
            new() { Name = "F-You Earth!", Description = "My second game jam entry", Price = new decimal(4.20d) },
            new() { Name = "Spacelancer", Description = "My nemesis", Price = new decimal(6.40d), Deleted = true },
        };

        context.Products.AddRange(products);
        context.SaveChanges();

        var sales = new Sale[]
        {
            new() { FirstName = "John", LastName = "Doe" },
            new() { FirstName = "Jane", LastName = "Doe" },
            new() { FirstName = "Deleted", LastName = "McDeletyFace", Deleted = true },
        };
        
        context.Sales.AddRange(sales);
        context.SaveChanges();

        var productSales = new ProductSale[]
        {
            new() { ProductID = 1, Quantity = 1, SaleID = 1 },
            new() { ProductID = 2, Quantity = 2, SaleID = 2 },
            new() { ProductID = 1, Quantity = 1, SaleID = 2 },
        };
        
        context.ProductSales.AddRange(productSales);
        context.SaveChanges();
    }
}