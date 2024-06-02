using EcommerceAPI.Entity.Exceptions;
using EcommerceAPI.Entity.Models;
using EcommerceAPI.Entity.PaginationModels;
using EcommerceAPI.Interface.IRepository.IEntitiesRepository;
using EcommerceAPI.Utils.Common;
using EcommerceAPI.Utils.Validation;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace EcommerceAPI.Repository.EntityRepository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync(PaginationParams request, bool trackChanges)
        {
            return await FindByCondition(c=>c.IsDeleted == false,trackChanges)
                  .OrderByDescending(c => c.Id)
                  .Skip((request.PageNumber - 1) * request.PageSize)
                  .Take(request.PageSize)
                  .ToListAsync();
        }

        public async Task<User> GetUserAsync(int Id, bool trackChanges)
        {
            #pragma warning disable CS8603 // Possible null reference return.
            return await FindByCondition(c=>c.Id.Equals(Id), trackChanges).SingleOrDefaultAsync();
            #pragma warning restore CS8603 // Possible null reference return.
        }

        public void CreateUser (User user)
        {
            user.Password = HashPassword.Encrypt(user.Password);
            user.CreatedBy = "Admin";
            user.CreatedOn = DateTime.Now;
            user.UpdatedBy = "Admin";
            user.UpdatedOn = DateTime.Now;
            user.IsActive = true;
            user.IsDeleted = false;
            Create(user);
        }
        public void DeleteUser(User user) => Delete(user);

        public async Task<User> FindUserNameAsync(string userName, bool trackChanges)
        {
            return await FindByCondition(c => c.Username.Equals(userName), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<int> CountAllUserAsync(bool trackChanges)
        {
            return await FindByCondition(c => c.IsDeleted == false, trackChanges).CountAsync();
        }
    }
}
