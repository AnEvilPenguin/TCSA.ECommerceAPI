using ECommerceAPI.DataSource;
using ECommerceAPI.Models;
using ECommerceAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;

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
    
    public IEnumerable<ProductSale>? GetSaleProduct(int id, int productId, int skip, int take)
    {
        if (!GetBaseSalesQuery().Any(s => s.ID == id))
            return null;

        return dbContext.ProductSales
            .Where(ps => ps.SaleID == id && ps.ProductID == productId)
            .OrderBy(ps => ps.ID)
            .Skip(skip)
            .Take(take);
    }

    public Sale? UpdateSale(int id, SaleUpdateDto sale)
    {
        if (string.IsNullOrWhiteSpace(sale.FirstName) || string.IsNullOrWhiteSpace(sale.LastName))
            throw new Exception("Both FirstName and LastName must be provided");

        var rows = GetBaseSalesQuery()
            .Where(s => s.ID == id)
            .ExecuteUpdate(s =>
                s.SetProperty(s => s.FirstName, sale.FirstName)
                    .SetProperty(s => s.LastName, sale.LastName));
        
        return rows < 1 ? null : dbContext.Sales.Single(s => s.ID == id);
    }

    public bool DeleteSale(int id)
    {
        var rows = dbContext.Sales
            .Where(s => s.ID == id)
            .ExecuteUpdate(s =>
                s.SetProperty(s => s.Deleted, true));

        return rows < 1;
    }

    private IQueryable<Sale> GetBaseSalesQuery() =>
        dbContext.Sales
        .Where(s => !s.Deleted);
}