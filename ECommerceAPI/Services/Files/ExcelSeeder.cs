using ECommerceAPI.Models;
using ECommerceAPI.Models.Options;

namespace ECommerceAPI.Services.Files;

public sealed class ExcelSeeder(ILogger<ExcelSeeder> logger, IExcelReader reader) : IExcelSeeder
{
    public IEnumerable<Product> GetProducts(SeedFileOptions options) =>
        reader.ReadAllRows<Product>(options.Path!, options.Sheet);

    public IEnumerable<Category> GetCategories(SeedFileOptions options) =>
        reader.ReadAllRows<Category>(options.Path!, options.Sheet);

    public IEnumerable<Sale> GetSales(SeedFileOptions options) =>
        reader.ReadAllRows<Sale>(options.Path!, options.Sheet);

    public IEnumerable<ProductSale> GetProductSales(SeedFileOptions options) =>
        reader.ReadAllRows<ProductSale>(options.Path!, options.Sheet);
}