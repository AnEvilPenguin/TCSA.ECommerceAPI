using ECommerceAPI.Models.DTOs;
using ECommerceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SaleController (ISaleService saleService) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<SaleDto>> GetProducts(int skip = 0, int take = 50)
    {
        if (skip < 0 || take < 1)
            return BadRequest();

        return Ok(saleService.GetSales(skip, take).Select(SaleDto.FromSale));
    }
}