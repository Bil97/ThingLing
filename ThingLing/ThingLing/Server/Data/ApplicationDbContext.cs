using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using ThingLing.Shared.Models;
using ThingLing.Shared.Models.UserAccount;

namespace ThingLing.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed database
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Id = "e2383d88-00cd-47af-a4b6-c78af61c46a2",
                    ConcurrencyStamp = "ed856075-aa15-4aeb-a9ca-6c4d85a21555"
                });

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER",
                    Id = "04ee1ebd-6fac-4750-91c8-e9b872f11eb7",
                    ConcurrencyStamp = "13ab7adf-d3e8-4787-ae29-a63ceac47b22"
                });
        }

        public DbSet<Shared.Models.Files.File> Files { get; set; }

        public DbSet<ContactForm> ContactForms { get; set; }

        public DbSet<Video> Videos { get; set; }
        public DbSet<Music> Music { get; set; }
        public DbSet<Software> Software { get; set; }
        public DbSet<ThreeDModel> ThreeDModels { get; set; }
    }
}
