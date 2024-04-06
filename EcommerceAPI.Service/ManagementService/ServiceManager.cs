using EcommerceAPI.Interface.IRepository;
using EcommerceAPI.Interface;
using EcommerceAPI.Interface.IService;
using EcommerceAPI.Interface.IService.IEntityService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceAPI.Service.EntityService;

namespace EcommerceAPI.Service.ManagementService
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserService> _userService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _userService = new Lazy<IUserService>(() => new UserService(repositoryManager, logger));

        }
        public IUserService UserService => _userService.Value;
    }
}
