using ECommerceAPI.Models;

namespace ECommerceAPI.Services;

public interface ISaleService
{
    public IEnumerable<Sale> GetSales(int skip, int take);
    public Sale? GetSale(int id);
}