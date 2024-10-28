using Information_System_ASP.Net.Data;
using Information_System_ASP.Net.Models;
using Information_System_ASP.Net.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Information_System_ASP.Net
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //  Add DbContext with SQL Server
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDB")));

            //  Add Identity services
            builder.Services.AddIdentity<Employee, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Konfigurera applikationen att omdirigera användare till inloggningssidan om de inte är autentiserade
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";  // Omdirigera till inloggningssidan om ej inloggad
                options.AccessDeniedPath = "/Account/AccessDenied";  // Omdirigera vid nekad åtkomst (kan användas för roller)
            });

            // Add controllers with views (since it's an MVC app)
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.Use(async (context, next) =>
            {
                // Add cache control headers to prevent caching of authenticated pages
                context.Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate, max-age=0";
                context.Response.Headers["Pragma"] = "no-cache";
                context.Response.Headers["Expires"] = "-1";

                await next();
            });


            app.UseRouting();

            //  Enable authentication and authorization
            app.UseAuthentication(); // Added for Identity
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");


            // Create a service scope to seed the roles and admin user
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                // The lambda here is marked as async to use await
                await DbSeeder.SeedRolesAndUsersAsync(services);
            }



            app.Run();

        }
    }
}
