using ECommerceAPI.Models;
using ECommerceAPI.Models.DTOs;
using ECommerceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<CategoryDTO>> GetCategories(int skip = 0, int take = 50)
    {
        if (skip < 0 || take < 1 || take > 100)
            return BadRequest();

        return Ok(categoryService.GetCategories(skip, take).Select(CategoryDTO.FromCategory));
    }

    [HttpGet("{id}")]
    public ActionResult<CategoryDTO> GetCategoryById(int id)
    {
        var category = categoryService.GetCategoryById(id);

        if (category == null)
            return NotFound();
        
        return CategoryDTO.FromCategory(category);
    }
    

    [HttpPut("{id}")]
    public ActionResult<CategoryDTO> UpdateCategory(int id, CategoryUpdateDTO categoryUpdateDto)
    {
        Category? result;

        try
        {
            result = categoryService.UpdateCategory(id, categoryUpdateDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
        if (result == null)
            return NotFound();

        return Ok(CategoryDTO.FromCategory(result));
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteCategory(int id) =>
        categoryService.DeleteCategory(id) ? Ok() : NotFound();
    
}