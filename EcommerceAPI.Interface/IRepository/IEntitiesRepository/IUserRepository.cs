using EcommerceAPI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Interface.IRepository.IEntitiesRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync(bool trackChanges);
        Task<User> GetUserAsync(int Id, bool trackChanges);
        Task<User> FindUserNameAsync(string userName, bool trackChanges);
        void CreateUser(User user);
        void DeleteUser(User user);
    }
}
