using EcommerceAPI.Entity.Models;
using EcommerceAPI.Interface.IRepository.IEntitiesRepository;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace EcommerceAPI.Repository.EntityRepository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        public async Task<IEnumerable<User>> GetAllUsers(bool trackChanges)
        {
            return await FindAll(trackChanges)
                  .OrderBy(c => c.FirstName)
                  .ToListAsync();
        }
    }
}
