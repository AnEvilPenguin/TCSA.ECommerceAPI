using ECommerceAPI.Models;
using ECommerceAPI.Models.DTOs;

namespace ECommerceAPI.Services;

public interface IProductService
{
    public IEnumerable<ProductDto> GetProducts(int skip, int take);
    public Product? GetProduct(int id);
}