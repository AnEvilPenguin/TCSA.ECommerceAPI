using ECommerceAPI.Models;
using ECommerceAPI.Models.DTOs;
using ECommerceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<ProductDto>> GetProducts(int skip = 0, int take = 50)
    {
        return Ok(productService.GetProducts(skip, take));
    }

    [HttpGet("{id}")]
    public ActionResult<ProductAndSaleDto> GetProduct(int id)
    {
        var result = productService.GetProduct(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(ProductAndSaleDto.FromProductAndSale(result));
    }
}