using Microsoft.EntityFrameworkCore;
using WebApplicationShop.Models;

namespace WebApplicationShop.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Role>().HasData(new Role { Id = 1, Name = "Admin" }, new Role { Id = 2, Name = "Manager" }, new Role { Id = 3, Name = "User" });
        }
    }
}