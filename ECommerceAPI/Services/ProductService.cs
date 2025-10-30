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
            .Where(p => !p.Deleted)
            .Skip(skip)
            .Take(take)
            .Select(p => ProductDto.FromProduct(p));
    }

    public Product? GetProduct(int id)
    {
        // might be doing too much here
        return dbContext.Products
            .Where(p => !p.Deleted)
            .Include(p => p.ProductSales.Where(ps => ps.ProductID == id))
            .ThenInclude(s => s.Sale)
            .AsNoTracking()
            .FirstOrDefault(p => p.ID == id);
    }

    public Product? PutProduct(ProductDto productDto)
    {
        throw new NotImplementedException();
    }

    public Product? PatchProduct(int id, object product)
    {
        throw new NotImplementedException();
    }
}