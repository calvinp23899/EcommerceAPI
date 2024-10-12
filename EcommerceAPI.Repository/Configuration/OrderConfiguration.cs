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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            #region Properties
            builder.ToTable("Order");
            builder.HasKey(o => o.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(p => p.OrderNumber)
                    .IsRequired(true)
                    .HasColumnType("varchar")
                    .HasMaxLength(40);
            builder.Property(p => p.PaymentMethod)
                    .IsRequired(true)
                    .HasColumnType("int");
            builder.Property(p => p.Tax)
                    .IsRequired(true)
                    .HasColumnType("decimal");
            builder.Property(p => p.TotalOrder)
                    .IsRequired(true)
                    .HasColumnType("decimal");
            builder.Property(p => p.Status)
                    .IsRequired(true)
                    .HasColumnType("int");
            builder.Property(p => p.AnonymousName)
                     .IsRequired(false)
                     .HasColumnType("varchar")
                     .HasMaxLength(255);
            builder.Property(p => p.AnonymousEmail)
                     .IsRequired(false)
                     .HasColumnType("varchar")
                     .HasMaxLength(255);
            builder.Property(p => p.AnonymousPhone)
                     .IsRequired(false)
                     .HasColumnType("varchar")
                     .HasMaxLength(255);
            builder.Property(p => p.AnonymousAddress)
                     .IsRequired(false)
                     .HasColumnType("varchar")
                     .HasMaxLength(255);
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
            builder.Property(p => p.IsDeleted)
                    .IsRequired(false)
                    .HasColumnType("bit");
            #endregion

            #region Relationship
            builder.HasOne(e => e.User).WithMany(c => c.Orders).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
