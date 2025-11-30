namespace ECommerceAPI.Models;

public class Category
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool Deleted { get; set; }
    public ICollection<Product>? Products { get; set; }
}