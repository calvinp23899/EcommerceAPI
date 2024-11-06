using EcommerceAPI.Entity.Models;
using EcommerceAPI.Interface;
using EcommerceAPI.Repository;
using EcommerceAPI.Utils.Common;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Extensions
{
    public static class PrepDataExtension
    {
        public static void SeedDataPopulation(IApplicationBuilder app, ILoggerManager logger)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                try
                {
                    var ApplicationContext = serviceScope.ServiceProvider.GetService<RepositoryContext>();
                    ApplicationContext.Database.Migrate();
                    logger.LogInfo("Migration data successful.");
                }
                catch (Exception ex)
                {
                    logger.LogInfo($"An error occurred while migrating the database: {ex.Message}");
                }
                SeedData(serviceScope.ServiceProvider.GetService<RepositoryContext>(), logger);
            }
        }
        private static void SeedData(RepositoryContext context, ILoggerManager logger)
        {
            //Add more data seed here
            if (!context.Users.Any())
            {
                logger.LogInfo($"Seeding data starting...");
                context.Users.AddRange(
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
                    Role = Entity.Enums.Role.USER,
                    IsActive = true,
                    IsDeleted = false,
                });
            }
            context.SaveChanges();
        }
    }
}
