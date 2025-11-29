using ECommerceAPI.Models;

namespace ECommerceAPI.Services.Files;

public interface ICsvSeeder
{
    public IEnumerable<Product> GetProducts(string path);
}