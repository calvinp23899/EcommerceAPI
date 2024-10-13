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
    public class VendorConfiguration : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {
            #region Properties
            builder.ToTable("Vendor");
            builder.HasKey(o => o.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(p => p.VendorName)
                .IsRequired(true)
                .HasColumnType("varchar")
                .HasMaxLength(255);
            builder.Property(p => p.IsDeleted)
                .IsRequired(false)
                .HasColumnType("bit");
            #endregion

            #region Relationship
            builder.HasMany(c => c.Products).WithOne(c => c.Vendor);
            #endregion
        }
    }
}
