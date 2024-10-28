using Information_System_ASP.Net.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Information_System_ASP.Net.Service
{
    public class DbSeeder
    {
        public static async Task SeedRolesAndUsersAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<Employee>>();

            // Seed roles
            string[] roleNames = { "Admin", "Employee" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Seed admin user
            await CreateUser(userManager, "admin@admin.com", "Admin", "Admin", "Admin@123");

            // Seed regular employee users
            await CreateUser(userManager, "jane.smith@company.com", "Jane Smith", "Employee", "Password@123");
            await CreateUser(userManager, "michael.brown@company.com", "Michael Brown", "Employee", "Password@123");
            await CreateUser(userManager, "alice.green@company.com", "Alice Green", "Employee", "Password@123");
            await CreateUser(userManager, "david.clark@company.com", "David Clark", "Employee", "Password@123");
            await CreateUser(userManager, "emma.white@company.com", "Emma White", "Employee", "Password@123");
        }

        // Helper method to create user and assign role
        private static async Task CreateUser(UserManager<Employee> userManager, string email, string name, string role, string password)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                var employee = new Employee
                {
                    UserName = email,
                    Email = email,
                    Name = name,
                    Role = role
                };

                var createResult = await userManager.CreateAsync(employee, password);
                if (createResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(employee, role);
                }
                else
                {
                    // Log or throw an exception with detailed error information
                    foreach (var error in createResult.Errors)
                    {
                        Console.WriteLine($"Error creating user {email}: {error.Description}");
                    }
                }
            }
        }
    }
}
