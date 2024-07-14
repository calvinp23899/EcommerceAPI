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
using AutoMapper;
using EcommerceAPI.Service.AuthService;
using Microsoft.Extensions.Configuration;
using EcommerceAPI.Entity.JwtModel;
using Microsoft.Extensions.Options;

namespace EcommerceAPI.Service.ManagementService
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IProductService> _productService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper, IConfiguration configuration)
        {
            _userService = new Lazy<IUserService>(() => new UserService(repositoryManager, logger, mapper));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(repositoryManager, logger, mapper, configuration));
            _productService = new Lazy<IProductService>(() => new ProductService(repositoryManager, logger, mapper));

        }
        public IUserService UserService => _userService.Value;
        public IProductService ProductService => _productService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;

    }
}
