using ECommerceAPI.Models;
using ECommerceAPI.Models.DTOs;
using ECommerceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SaleController(ISaleService saleService) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<SaleDto>> GetProducts(int skip = 0, int take = 50)
    {
        if (skip < 0 || take < 1 || take > 100)
            return BadRequest();

        return Ok(saleService.GetSales(skip, take).Select(SaleDto.FromSale));
    }

    [HttpGet("{id}")]
    public ActionResult<SaleDto> GetSale(int id)
    {
        var result = saleService.GetSale(id);

        if (result == null)
            return NotFound();

        return Ok(SaleDto.FromSale(result));
    }

    [HttpGet("{id}/saleProducts")]
    public ActionResult<IEnumerable<ProductSaleDto>> GetSaleProducts(int id, int? productId, int skip = 0,
        int take = 50)
    {
        if (skip < 0 || take < 1)
            return BadRequest();

        var sales = productId == null
            ? saleService.GetSaleProduct(id, skip, take)
            : saleService.GetSaleProduct(id, productId.Value, skip, take);

        if (sales == null)
            return NotFound();

        return Ok(sales.Select(ProductSaleDto.FromProductSale));
    }
    
    [HttpGet("{id}/saleValue")]
    public ActionResult<decimal> GetSaleValue(int id)
    {
        var value = saleService.GetSaleValue(id);

        if (value == null)
            return NotFound();

        return Ok(value);
    }
    

    [HttpPut("{id}")]
    public ActionResult<ProductDto> UpdateProduct(int id, [FromBody] SaleUpdateDto saleUpdateDto)
    {
        Sale? result;
        try
        {
            result = saleService.UpdateSale(id, saleUpdateDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        if (result == null)
            return NotFound();

        return Ok(SaleDto.FromSale(result));
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteSale(int id) =>
        saleService.DeleteSale(id) ? Ok() : NotFound();
}