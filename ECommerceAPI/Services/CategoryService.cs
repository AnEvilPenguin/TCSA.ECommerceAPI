using ECommerceAPI.DataSource;
using ECommerceAPI.Models;

namespace ECommerceAPI.Services;

public class CategoryService(CommerceContext dbContext) : ICategoryService
{
    public IEnumerable<Category> GetCategories(int skip, int take)
    {
        return GetBaseCategoryQuery()
            .OrderBy(c => c.ID)
            .Skip(skip)
            .Take(take);
    }

    public Category? GetCategoryById(int id)
    {
        return GetBaseCategoryQuery()
            .FirstOrDefault(c => c.ID == id);
    }


    private IQueryable<Category> GetBaseCategoryQuery() =>
        dbContext.Categories
            .Where(s => !s.Deleted);
}