using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ProductWebAPI;
using ProductWebAPI.Models;
using System.Reflection.Emit;


namespace ProductWebAPI.Data
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<UserProduct> UserProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserProduct>(x => x.HasKey(p => new { p.UserId, p.ProductId }));

            builder.Entity<UserProduct>().HasOne(u => u.User).WithMany(u => u.UserProducts).HasForeignKey(p => p.UserId);

            builder.Entity<UserProduct>().HasOne(u => u.Product).WithMany(u => u.UserProducts).HasForeignKey(p => p.ProductId);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
