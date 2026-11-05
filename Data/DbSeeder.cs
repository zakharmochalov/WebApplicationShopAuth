using Microsoft.AspNetCore.Identity;
using WebApplicationShop.Models;

namespace WebApplicationShop.Data
{
    public static class DbSeeder
    {
        public static void SeedUsers(AppDbContext db)
        {
            if (db.Users.Any()) return;

            var hasher = new PasswordHasher<User>();

            var admin = new User { Email = "admin@shop.ru", RoleId = 1 };     // Admin
            var manager = new User { Email = "manager@shop.ru", RoleId = 2 }; // Manager
            var user = new User { Email = "user@shop.ru", RoleId = 3 };       // User

            admin.PasswordHash = hasher.HashPassword(admin, "admin123");
            manager.PasswordHash = hasher.HashPassword(manager, "manager123");
            user.PasswordHash = hasher.HashPassword(user, "user123");

            db.Users.AddRange(admin, manager, user);
            db.SaveChanges();
        }
    }
}