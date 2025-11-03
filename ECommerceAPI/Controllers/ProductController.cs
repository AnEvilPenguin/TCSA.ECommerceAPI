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
    public ActionResult<IEnumerable<ProductDto>> GetProducts(int skip = 0, int take = 50, int? categoryId = null)
    {
        if (skip < 0 || take < 1 || take > 100)
            return BadRequest();
        
        var products = categoryId == null ?
            productService.GetProducts(skip, take) :
            productService.GetProducts(categoryId.Value, skip, take);

        return Ok(products.Select(ProductDto.FromProduct));
    }

    [HttpGet("{id}")]
    public ActionResult<ProductDto> GetProduct(int id)
    {
        var result = productService.GetProduct(id);

        if (result == null)
            return NotFound();

        return Ok(ProductDto.FromProduct(result));
    }

    [HttpGet("{id}/productSales")]
    public ActionResult<IEnumerable<ProductSaleDto>> GetProductSales(int id, int skip = 0, int take = 50)
    {
        if (skip < 0 || take < 1)
            return BadRequest();

        var sales = productService.GetProductSale(id, skip, take);

        if (sales == null)
            return NotFound();

        return Ok(sales.Select(ProductSaleDto.FromProductSale));
    }

    [HttpPut("{id}")]
    public ActionResult<ProductDto> UpdateProduct(int id, [FromBody] ProductUpdateDto productUpdateDto)
    {
        Product? result;
        try
        {
            result = productService.UpdateProduct(id, productUpdateDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        if (result == null)
            return NotFound();

        return Ok(ProductDto.FromProduct(result));
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteProduct(int id) =>
        productService.DeleteProduct(id) ? Ok() : NotFound();
}