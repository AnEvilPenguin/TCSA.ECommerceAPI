using ECommerceAPI.Models;
using ECommerceAPI.Models.Options;
using ECommerceAPI.Services.Files;
using Microsoft.Extensions.Options;

namespace ECommerceAPI.DataSource;

public class DbInitializer (CommerceContext context, IOptions<SeedDataOptions> configuration, IFileSeeder _seeder) : IDbInitialzer
{
    private readonly SeedDataOptions _options = configuration.Value;
    
    public void Initialize()
    {
        context.Database.EnsureCreated();
        
        if (!_options.Enabled)
            return;

        if (!context.Products.Any() && _options.Products != null)
        {
            var products = _seeder.GetProducts();
            
            context.Products.AddRange(products);
            context.SaveChanges();
        }
            
        
        // TODO check configuration
        // if no seed data use static ones
        // else start processing files
    }

    private static void StaticSeeding(CommerceContext context)
    {
        Product sa = new() { Name = "Shadow Alchemist: Alchemy in the Shadows", Price = new decimal(3.50d) };
        Product fe = new()
            { Name = "F-You Earth!", Description = "My second game jam entry", Price = new decimal(4.20d) };


        var products = new Product[]
        {
            sa, fe,
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

        var categories = new Category[]
        {
            new() { Name = "RogueLite", Description = "Rogue Lite", Products = [sa, fe] },
            new() { Name = "Action", Products = [fe] },
            new() { Name = "Top Down" },
            new() { Name = "Fantasy", Deleted = true, Products = [sa] },
        };
        
        context.Categories.AddRange(categories);
        context.SaveChanges();
    }
}