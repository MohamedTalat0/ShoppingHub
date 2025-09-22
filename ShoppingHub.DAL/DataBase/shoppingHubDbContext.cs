using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingHub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.DAL.DataBase
{
    public class shoppingHubDbContext: IdentityDbContext<User>
    {
        public shoppingHubDbContext(DbContextOptions<shoppingHubDbContext> options) :base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CartItem>().HasKey(a=> new {a.ProductID,a.UserID });
            modelBuilder.Entity<OrderItem>().HasKey(a => new { a.ProductID, a.OrderID });
            modelBuilder.Entity<ProductRating>().HasKey(a => new { a.ProductID, a.UserID });

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductRating> ProductRatings { get; set; }
    }
}
