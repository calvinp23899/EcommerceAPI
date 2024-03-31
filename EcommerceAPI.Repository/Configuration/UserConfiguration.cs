using EcommerceAPI.Entity.Models;
using EcommerceAPI.Utils.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EcommerceAPI.Repository.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            #region Properties
            builder.ToTable("User");
            builder.HasKey(o => o.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Username)
                    .IsRequired(true)
                    .HasColumnType("varchar")
                    .HasMaxLength(60);
            builder.Property(p => p.Password)
                    .IsRequired(true)
                    .HasColumnType("varchar")
                    .HasMaxLength(250);
            builder.Property(p => p.FirstName)
                    .IsRequired(true)
                    .HasColumnType("varchar")
                    .HasMaxLength(250);
            builder.Property(p => p.LastName)
                    .IsRequired(true)
                    .HasColumnType("varchar")
                    .HasMaxLength(250);
            builder.Property(p => p.Email)
                    .IsRequired(true)
                    .HasColumnType("varchar")
                    //.HasAnnotation("Index", new IndexAttribute() { IsUnique = true })
                    .HasMaxLength(250);        
            builder.Property(p => p.PhoneNumber)
                    .IsRequired(true)
                    .HasColumnType("varchar")
                    //.HasAnnotation("Index", new IndexAttribute() { IsUnique = true })
                    .HasMaxLength(250);
            builder.Property(p => p.Address)
                    .IsRequired(true)
                    .HasColumnType("varchar")
                    .HasMaxLength(250);
            builder.Property(p => p.DateOfBirth)
                    .HasColumnType("datetime");
            builder.Property(p => p.Role)
                    .IsRequired(true)
                    .HasColumnType("int");
            builder.Property(p => p.RefreshToken)
                    .HasMaxLength(255);
            builder.Property(p => p.RefreshTokenExpiryTime)
                    .IsRequired(false)
                    .HasColumnType("date");
            builder.Property(p => p.IsActive)
                    .IsRequired(true)
                    .HasColumnType("bit");
            builder.Property(p => p.IsDeleted)
                    .HasColumnType("bit");
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
            builder.HasMany(c => c.Orders).WithOne(e => e.User);
            #endregion
            #region Data
            builder.HasData
            (
                new User
                {
                    Id = 1,
                    Username = "superadmin",
                    Password = HashPassword.Encrypt("Admin123@"),
                    FirstName = "super",
                    LastName = "admin",
                    Email = "admin@gmail.com",
                    PhoneNumber = "012345678",
                    Address = "123 LA",
                    DateOfBirth = DateTime.Parse("1999-08-23"),
                    CreatedBy = "System",
                    CreatedOn = DateTime.Now,
                    UpdatedBy = "System",
                    UpdatedOn = DateTime.Now,
                    Role = Entity.Enums.Role.SUPER_ADMIN,
                    IsActive = true,
                    IsDeleted = false,
                },
                new User
                {
                    Id = 2,
                    Username = "user1",
                    Password = HashPassword.Encrypt("User123@"),
                    FirstName = "user1",
                    LastName = "user1",
                    Email = "user1@gmail.com",
                    PhoneNumber = "0987654321",
                    Address = "1234 SA",
                    DateOfBirth = DateTime.Parse("1999-08-23"),
                    CreatedBy = "System",
                    CreatedOn = DateTime.Now,
                    UpdatedBy = "System",
                    UpdatedOn = DateTime.Now,
                    Role = Entity.Enums.Role.SUPER_ADMIN,
                    IsActive = true,
                    IsDeleted = false,
                }
            );
            #endregion
        }
    }
}
