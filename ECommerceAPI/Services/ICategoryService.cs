using ECommerceAPI.Models;

namespace ECommerceAPI.Services;

public interface ICategoryService
{
    public IEnumerable<Category> GetCategories(int skip, int take);
    public Category? GetCategoryById(int id);
}