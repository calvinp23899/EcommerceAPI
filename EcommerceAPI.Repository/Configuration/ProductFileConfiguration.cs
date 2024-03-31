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
    public class ProductFileConfiguration : IEntityTypeConfiguration<ProductFile>
    {
        public void Configure(EntityTypeBuilder<ProductFile> builder)
        {
            #region Properties
            builder.ToTable("ProductFile");
            builder.HasKey(o => o.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(p => p.FileName)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(255);
            builder.Property(p => p.FilePath)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(255);
            builder.Property(p => p.CreatedBy)
                    .HasColumnType("varchar")
                    .HasMaxLength(250);
            builder.Property(p => p.CreatedOn)
                    .HasColumnType("datetime");
            builder.Property(p => p.UpdatedBy)
                    .HasColumnType("varchar")
                    .HasMaxLength(250);
            builder.Property(p => p.UpdatedOn)
                    .HasColumnType("datetime");
            #endregion

            #region Relationship
            builder.HasOne(e => e.Product).WithMany(c => c.ProductFiles).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
