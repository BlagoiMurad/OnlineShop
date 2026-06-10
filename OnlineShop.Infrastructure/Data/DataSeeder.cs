using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Entities;

namespace OnlineShop.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            
            if (!await roleManager.RoleExistsAsync("Administrator"))
                await roleManager.CreateAsync(new IdentityRole("Administrator"));

            if (!await roleManager.RoleExistsAsync("User"))
                await roleManager.CreateAsync(new IdentityRole("User"));

            
            if (await userManager.FindByEmailAsync("admin@onlineshop.com") == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = "admin@onlineshop.com",
                    Email = "admin@onlineshop.com",
                    FirstName = "Admin",
                    LastName = "User",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(admin, "Admin123!");
                await userManager.AddToRoleAsync(admin, "Administrator");
            }

            
            if (!await context.Categories.AnyAsync())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Electronics", Description = "Electronic devices and accessories" },
                    new Category { Name = "Clothing", Description = "Men and women clothing" },
                    new Category { Name = "Books", Description = "Books and magazines" },
                    new Category { Name = "Sports", Description = "Sports equipment and accessories" }

                };
                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }

        
            if (!await context.Products.AnyAsync())
            {
                var electronics = await context.Categories.FirstAsync(c => c.Name == "Electronics");
                var clothing = await context.Categories.FirstAsync(c => c.Name == "Clothing");
                var books = await context.Categories.FirstAsync(c => c.Name == "Books");
                var sports = await context.Categories.FirstAsync(c => c.Name == "Sports");

                var products = new List<Product>
                {
                    new Product { Name = "Laptop", Description = "High performance laptop", Price = 999.99m, Stock = 10, CategoryId = electronics.Id },
                    new Product { Name = "Smartphone", Description = "Latest smartphone", Price = 699.99m, Stock = 25, CategoryId = electronics.Id },
                    new Product { Name = "T-Shirt", Description = "Cotton t-shirt", Price = 19.99m, Stock = 100, CategoryId = clothing.Id },
                    new Product { Name = "Jeans", Description = "Blue denim jeans", Price = 49.99m, Stock = 50, CategoryId = clothing.Id },
                    new Product { Name = "C# Programming", Description = "Learn C# programming", Price = 29.99m, Stock = 30, CategoryId = books.Id },
                    new Product { Name = "Football", Description = "Professional football", Price = 24.99m, Stock = 40, CategoryId = sports.Id },
                    new Product { Name = "Headphones", Description = "Wireless headphones", Price = 149.99m, Stock = 15, CategoryId = electronics.Id },
new Product { Name = "Keyboard", Description = "Mechanical keyboard", Price = 89.99m, Stock = 20, CategoryId = electronics.Id },
new Product { Name = "Jacket", Description = "Winter jacket", Price = 79.99m, Stock = 30, CategoryId = clothing.Id },
new Product { Name = "Sneakers", Description = "Running sneakers", Price = 59.99m, Stock = 45, CategoryId = clothing.Id },
new Product { Name = "ASP.NET Core", Description = "Learn ASP.NET Core", Price = 34.99m, Stock = 25, CategoryId = books.Id },
new Product { Name = "Basketball", Description = "Professional basketball", Price = 29.99m, Stock = 35, CategoryId = sports.Id },
                };
                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }
    }
}