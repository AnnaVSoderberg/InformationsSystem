using Information_System_ASP.Net.Models;
using Information_System_ASP.Net.Service;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Information_System_ASP.Net.Data
{
    public class ApplicationDbContext : IdentityDbContext<Employee>
    {
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Event> Events { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Define relationships between models
            builder.Entity<Event>()
                .HasOne(e => e.Driver)
                .WithMany(d => d.Events)
                .HasForeignKey(e => e.DriverID);

            // Seed data
            SeedData.Seed(builder);
        }
    }
}
