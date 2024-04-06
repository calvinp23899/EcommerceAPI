
using EcommerceAPI.Entity.Models;

namespace EcommerceAPI.Interface.IService.IEntityService
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers(bool trackChanges);
    }
}
