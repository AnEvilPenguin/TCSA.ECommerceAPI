using ECommerceAPI.Models;
using ECommerceAPI.Models.DTOs;

namespace ECommerceAPI.Services;

public interface ICategoryService
{
    public IEnumerable<Category> GetCategories(int skip, int take);
    public Category? GetCategoryById(int id);
    public Category? UpdateCategory(int id, CategoryUpdateDTO category);
    public bool DeleteCategory(int id);
}