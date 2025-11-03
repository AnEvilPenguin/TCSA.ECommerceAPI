using ECommerceAPI.DataSource;
using ECommerceAPI.Models;
using ECommerceAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;

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

    public Category? UpdateCategory(int id, CategoryUpdateDTO category)
    {
        var rows = dbContext.Categories
            .Where(c => c.ID == id)
            .ExecuteUpdate(s =>
                s.SetProperty(c => c.Name, category.Name)
                    .SetProperty(c => c.Description, category.Description));
        
        return rows < 1 ? null : dbContext.Categories.Single(c => c.ID == id);
    }

    public bool DeleteCategory(int id)
    {
        var rows = dbContext.Categories
            .Where(c => c.ID == id)
            .ExecuteUpdate(s =>
                s.SetProperty(c => c.Deleted, true));
        
        return rows < 1;
    }


    private IQueryable<Category> GetBaseCategoryQuery() =>
        dbContext.Categories
            .Where(s => !s.Deleted);
}