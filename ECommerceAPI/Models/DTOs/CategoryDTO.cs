namespace ECommerceAPI.Models.DTOs;

public class CategoryDTO
{
    public int ID { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public static CategoryDTO FromCategory(Category category) =>
        new CategoryDTO
        {
            ID = category.ID,
            Name = category.Name,
            Description = category.Description
        };
}

public class CategoryUpdateDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
}