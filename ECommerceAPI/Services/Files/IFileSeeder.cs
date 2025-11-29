using ECommerceAPI.Models;

namespace ECommerceAPI.Services.Files;

public interface IFileSeeder
{
    public IEnumerable<Product> GetProducts();
}