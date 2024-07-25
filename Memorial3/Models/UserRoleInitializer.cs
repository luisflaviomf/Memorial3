using Microsoft.EntityFrameworkCore.Metadata;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace Memorial3.Models
{
    public static class UserRoleInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<DefaultUser>>();

            string[] roleNames = { "Admin", "User" };

            IdentityResult roleResult;
            foreach (var role in roleNames) { 
                var roleExists = await roleManager.RoleExistsAsync(role);

                if (!roleExists) {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var email = "admin@site.com";
            var password = "123456";

            if (userManager.FindByEmailAsync(email).Result == null) {
                DefaultUser user = new()
                {
                    Email = email,
                    UserName = email,
                    FirstName = "admin",
                    LastName = "adminson",
                    City = "Cuiabá",
                    State = "Mato Grosso",
                    Country = "Brasil"
                };
                IdentityResult result = userManager.CreateAsync(user, password).Result;

                if (result.Succeeded) { 
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
