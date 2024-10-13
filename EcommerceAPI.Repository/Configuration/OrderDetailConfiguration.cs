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
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            #region Properties
            builder.ToTable("OrderDetail");
            builder.HasKey(o => o.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(p => p.UnitPrice)
                .IsRequired(true)
                .HasColumnType("decimal");
            builder.Property(p => p.Quantity)
                .IsRequired(true)
                .HasColumnType("int");
            builder.Property(p => p.Total)
                .IsRequired(true)
                .HasColumnType("decimal");
            builder.Property(p => p.IsDeleted)
                    .IsRequired(false)
                    .HasColumnType("bit");
            #endregion

            #region Relationship
            builder.HasOne(e => e.Product).WithMany(c => c.OrderDetails).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade); 
            builder.HasOne(e => e.Order).WithMany(c => c.OrderDetails).HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
