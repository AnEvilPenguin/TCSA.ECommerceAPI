using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Models;

public class CommerceContext : DbContext
{
    public CommerceContext(DbContextOptions<CommerceContext> options) : base(options)
    {}
    
    public DbSet<ProductSale> ProductSales { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Sale> Sales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductSale>().ToTable("ProductSale");
        modelBuilder.Entity<Product>().ToTable("Product");
        modelBuilder.Entity<Sale>().ToTable("Sale");
    }
}