using ECommerceAPI.Models;
using ECommerceAPI.Models.DTOs;

namespace ECommerceAPI.Services;

public interface IProductService
{
    public IEnumerable<Product> GetProducts(int skip, int take);
    public IEnumerable<Product> GetProducts(int categoryId, int skip, int take);
    public Product? GetProduct(int id);
    public IEnumerable<ProductSale>? GetProductSale(int id, int skip, int take);
    public Product? UpdateProduct(int id, ProductUpdateDto productUpdateDto);
    public bool DeleteProduct(int id);
}