using ECommerceAPI.DataSource;
using ECommerceAPI.Models;
using ECommerceAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Services;

public class ProductService(CommerceContext dbContext) : IProductService
{
    public IEnumerable<ProductDto> GetProducts(int skip = 0, int take = 50)
    {
        return dbContext.Products
            .Skip(skip)
            .Take(take)
            .Select(p => ProductDto.FromProduct(p));
    }

    public Product? GetProduct(int id)
    {
        return dbContext.Products
            .Include(p => p.ProductSales.Where(ps => ps.ProductID == id))
            .ThenInclude(s => s.Sale)
            .AsNoTracking()
            .FirstOrDefault(p => p.ID == id);
    }
}