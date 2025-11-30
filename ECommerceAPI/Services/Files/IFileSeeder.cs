using ECommerceAPI.Models;

namespace ECommerceAPI.Services.Files;

public interface IFileSeeder
{
    public IEnumerable<Product> GetProducts();
    public IEnumerable<Category> GetCategories();
    public IEnumerable<Sale> GetSales();
    public IEnumerable<ProductSale> GetProductSales();
}