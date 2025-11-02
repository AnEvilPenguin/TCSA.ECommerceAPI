using ECommerceAPI.Models;
using ECommerceAPI.Models.DTOs;

namespace ECommerceAPI.Services;

public interface ISaleService
{
    public IEnumerable<Sale> GetSales(int skip, int take);
    public Sale? GetSale(int id);
    public IEnumerable<ProductSale>? GetSaleProduct(int id, int skip, int take);
    public IEnumerable<ProductSale>? GetSaleProduct(int id, int productId, int skip, int take);
    public decimal? GetSaleValue(int id);
    public Sale? UpdateSale(int id, SaleUpdateDto sale);
    public bool DeleteSale(int id);
}