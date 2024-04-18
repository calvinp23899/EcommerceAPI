
using EcommerceAPI.Entity.DTOs;
using EcommerceAPI.Entity.Models;
using EcommerceAPI.Entity.PaginationModels;

namespace EcommerceAPI.Interface.IService.IEntityService
{
    public interface IUserService
    {
        Task<(IEnumerable<UserDto>, int)> GetAllUsersAsync(PaginationParams request, bool trackChanges);
        Task<UserDto> GetUserAsync(int Id, bool trackChanges);
        Task<UserDto> CreateUserAsync(UserCreationDto user);
        Task DeleteUserAsync(int Id, UserDeleteDto user, bool trackChanges);
        Task UpdateUserAsync(int Id, UserUpdateDto user, bool trackChanges);
    }
}
