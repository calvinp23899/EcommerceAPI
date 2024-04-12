using EcommerceAPI.Interface.IService.IEntityService;

namespace EcommerceAPI.Interface.IService
{
    public interface IServiceManager
    {
        IUserService UserService { get; }
        IAuthenticationService AuthenticationService { get; }
    }
}
