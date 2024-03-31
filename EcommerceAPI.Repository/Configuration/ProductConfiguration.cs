using EcommerceAPI.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Repository.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            #region Properties
            builder.ToTable("Product");
            builder.HasKey(o => o.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(p => p.ProductName)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(255);
            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal");
            builder.Property(p => p.Quantity)
                .IsRequired()
                .HasColumnType("int");
            builder.Property(p => p.Description)
                .IsRequired()
                .HasColumnType("varchar");
            builder.Property(p => p.IsActive)
                .IsRequired()
                .HasColumnType("bit");
            builder.Property(p => p.IsActive)
                .HasColumnType("bit");
            builder.Property(p => p.IsHot)
                .HasColumnType("bit");
            builder.Property(p => p.CreatedBy)
                    .HasColumnType("varchar")
                    .HasMaxLength(250);
            builder.Property(p => p.CreatedOn)
                    .IsRequired()
                    .HasColumnType("datetime");
            builder.Property(p => p.UpdatedBy)
                    .HasColumnType("varchar")
                    .HasMaxLength(250);
            builder.Property(p => p.UpdatedOn)
                    .IsRequired()
                    .HasColumnType("datetime");
            #endregion

            #region Relationship
            builder.HasOne(e => e.Vendor).WithMany(c => c.Products).HasForeignKey(x => x.VendorId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(e => e.OrderDetails).WithOne(e => e.Product);
            #endregion

        }
    }
}
