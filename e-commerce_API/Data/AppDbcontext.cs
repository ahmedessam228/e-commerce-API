using e_commerce_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Emit;

namespace e_commerce_API.Data
{
    public class AppDbcontext : IdentityDbContext<ApplicationUser>
    {
        public AppDbcontext() { }
        public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var constr = config.GetSection("constr").Value;

            optionsBuilder.UseSqlServer(constr);    

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Category to Products (One-to-Many)
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.category)
                .HasForeignKey(p => p.C_id);

            // Orders to ApplicationUser (One-to-Many)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.ApplicationUser)
                .WithMany(u => u.orders)
                .HasForeignKey(o => o.UserId);


            // (Order to Product Many-to-Many) as OrderDetails
            modelBuilder.Entity<OrderDetails>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.O_id);

            modelBuilder.Entity<OrderDetails>()
                .HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId);

        }
    }
}
