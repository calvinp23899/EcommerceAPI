using EcommerceAPI.Entity.Models;
using EcommerceAPI.Repository.Configuration;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new VendorConfiguration());
            modelBuilder.ApplyConfiguration(new ProductFileConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
        }
        #region DbSets
        public DbSet<User>? Users { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Vendor>? Vendors { get; set; }
        public DbSet<ProductFile>? ProductFiles { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<OrderDetail>? OrderDetails { get; set; }
        #endregion

    }
}
