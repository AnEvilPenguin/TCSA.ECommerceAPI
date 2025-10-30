using ECommerceAPI.Models;
using ECommerceAPI.Models.DTOs;
using ECommerceAPI.Services;
using Microsoft.AspNetCore.JsonPatch;
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
            return NotFound();

        return Ok(ProductAndSaleDto.FromProductAndSale(result));
    }

    [HttpPut("{id}")]
    public ActionResult PutProduct(int id, [FromBody] ProductUpdateDTO productDto)
    {
        throw new NotImplementedException();
    }

    [HttpPatch("{id}")]
    public ActionResult PatchProduct(int id, [FromBody] object? patchObject)
    {
        if (patchObject == null)
            return BadRequest();
        
        var currentProduct = productService.GetProduct(id);
        
        if (currentProduct == null)
            return NotFound();
        
        
        throw new NotImplementedException();
    }
}