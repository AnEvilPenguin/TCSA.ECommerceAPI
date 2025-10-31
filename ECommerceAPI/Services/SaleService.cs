using ECommerceAPI.DataSource;
using ECommerceAPI.Models;

namespace ECommerceAPI.Services;

public class SaleService(CommerceContext dbContext) : ISaleService
{
    public IEnumerable<Sale> GetSales(int skip, int take)
    {
        return GetBaseSalesQuery()
            .OrderBy(s => s.ID)
            .Skip(skip)
            .Take(take);
    }

    public Sale? GetSale(int id)
    {
        return GetBaseSalesQuery()
            .FirstOrDefault(s => s.ID == id);
    }

    public IEnumerable<ProductSale>? GetSaleProduct(int id, int skip, int take)
    {
        if (!GetBaseSalesQuery().Any(s => s.ID == id))
            return null;

        return dbContext.ProductSales
            .Where(ps => ps.SaleID == id)
            .OrderBy(ps => ps.ID)
            .Skip(skip)
            .Take(take);
    }
    
    private IQueryable<Sale> GetBaseSalesQuery() =>
        dbContext.Sales
        .Where(s => !s.Deleted);
}