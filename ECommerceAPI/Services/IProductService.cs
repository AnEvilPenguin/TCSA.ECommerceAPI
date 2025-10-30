using ECommerceAPI.Models;
using ECommerceAPI.Models.DTOs;

namespace ECommerceAPI.Services;

public interface IProductService
{
    public IEnumerable<ProductDto> GetProducts(int skip, int take);
    public Product? GetProduct(int id);
    public Product? PutProduct(ProductDto productDto);
    public Product? PatchProduct(int id, object product);
}