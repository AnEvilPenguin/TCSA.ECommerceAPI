using ECommerceAPI.Models;
using ECommerceAPI.Models.Options;

namespace ECommerceAPI.Services.Files;

public interface ISeeder
{
    // TODO make these generic?
    // Could make each model have a common ancestor for limiting down
    // IModel/AbstractModel or something
    public IEnumerable<Product> GetProducts(SeedFileOptions options);
    public IEnumerable<Category> GetCategories(SeedFileOptions options);
    public IEnumerable<Sale> GetSales(SeedFileOptions options);
    public IEnumerable<ProductSale> GetProductSales(SeedFileOptions options);
}