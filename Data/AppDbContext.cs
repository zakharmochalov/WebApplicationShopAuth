using Microsoft.EntityFrameworkCore;
using WebApplicationShop.Models;

namespace WebApplicationShop.Data
{ 
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
        }
    }
}