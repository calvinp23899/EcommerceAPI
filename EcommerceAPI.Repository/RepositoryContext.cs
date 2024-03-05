using EcommerceAPI.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options){}
        public DbSet<User>? Users { get; set; }

    }
}
