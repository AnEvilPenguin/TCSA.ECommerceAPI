using ECommerceAPI.DataSource;
using ECommerceAPI.Models;

namespace ECommerceAPI.Services;

public class SaleService(CommerceContext dbContext) : ISaleService
{
    public IEnumerable<Sale> GetSales(int skip, int take)
    {
        return dbContext.Sales
            .Where(s => !s.Deleted)
            .OrderBy(s => s.ID)
            .Skip(skip)
            .Take(take);
    }

    public Sale? GetSale(int id)
    {
        return dbContext.Sales
            .Where(s => !s.Deleted)
            .FirstOrDefault(s => s.ID == id);
    }
}