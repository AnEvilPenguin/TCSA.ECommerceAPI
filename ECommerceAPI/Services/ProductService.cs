using ECommerceAPI.DataSource;
using ECommerceAPI.Models;
using ECommerceAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Services;

public class ProductService(CommerceContext dbContext) : IProductService
{
    public IEnumerable<Product> GetProducts(int skip = 0, int take = 50)
    {
        return dbContext.Products
            .Where(p => !p.Deleted)
            .OrderBy(p => p.ID)
            .Skip(skip)
            .Take(take);
    }

    public Product? GetProduct(int id)
    {
        return dbContext.Products
            .Where(p => !p.Deleted)
            .FirstOrDefault(p => p.ID == id);
    }

    public IEnumerable<ProductSale>? GetProductSale(int id, int skip = 0, int take = 50)
    {
        if (!dbContext.Products.Any(p => p.ID == id))
            return null;

        return dbContext.ProductSales
            .Where(ps => ps.ProductID == id)
            .OrderBy(ps => ps.ID)
            .Skip(skip)
            .Take(take);
    }

    public Product? UpdateProduct(int id, ProductUpdateDto product)
    {
        if (product.Price < 0)
            throw new Exception("Pice cannot be negative");

        var rows = dbContext.Products
            .Where(p => p.ID == id)
            .ExecuteUpdate(s =>
                s.SetProperty(p => p.Name, p => product.Name)
                    .SetProperty(p => p.Description, p => product.Description)
                    .SetProperty(p => p.Price, p => product.Price)
            );

        return rows < 1 ? null : dbContext.Products.Single(p => p.ID == id);
    }

    public bool DeleteProduct(int id)
    {
        var rows = dbContext.Products
            .Where(p => p.ID == id)
            .ExecuteUpdate(s =>
                s.SetProperty(p => p.Deleted, p => true));

        return rows < 1;
    }
}