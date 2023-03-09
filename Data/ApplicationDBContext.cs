using Microsoft.EntityFrameworkCore;
using MockProject.Models;
using MockProject.Models.User;

namespace MockProject.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<LocalUser> LocalUser { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Cart { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Product 1", Review = "Good", Price = 100, RemainingQuantity = 10 },
                new Product { Id = 2, Name = "Product 2", Review = "Excellent", Price = 200, RemainingQuantity = 20 },
                new Product { Id = 3, Name = "Product 3", Review = "Average", Price = 50, RemainingQuantity = 5 },
                new Product { Id = 4, Name = "Product 4", Review = "Good", Price = 80, RemainingQuantity = 15 },
                new Product { Id = 5, Name = "Product 5", Review = "Excellent", Price = 150, RemainingQuantity = 25 },
                new Product { Id = 6, Name = "Product 6", Review = "Good", Price = 90, RemainingQuantity = 10 },
                new Product { Id = 7, Name = "Product 7", Review = "Average", Price = 60, RemainingQuantity = 8 },
                new Product { Id = 8, Name = "Product 8", Review = "Good", Price = 70, RemainingQuantity = 12 },
                new Product { Id = 9, Name = "Product 9", Review = "Excellent", Price = 250, RemainingQuantity = 30 },
                new Product { Id = 10, Name = "Product 10", Review = "Good", Price = 120, RemainingQuantity = 18 }
                );
            modelBuilder.Entity<LocalUser>().HasData(
                new LocalUser {Id = 1, Email = "nguyentunglam2410@gmail.com", Name = "Nguyen Tung Lam", Password = "lam55526", Role = "Admin", UserName = "tunglam24"}
                );
            modelBuilder.Entity<Cart>()
                .HasKey(i => new { i.UserId, i.ProductId });
            modelBuilder.Entity<Cart>()
                .HasOne(i => i.User)
                .WithMany(i => i.Cart)
                .HasForeignKey(i => i.UserId);
            modelBuilder.Entity<Cart>()
                .HasOne(i => i.Product)
                .WithMany(i => i.Cart)
                .HasForeignKey(i => i.ProductId);
        }
    }
}
