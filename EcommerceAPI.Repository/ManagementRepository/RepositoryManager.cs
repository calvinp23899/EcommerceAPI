using EcommerceAPI.Interface.IRepository;
using EcommerceAPI.Interface.IRepository.IEntitiesRepository;
using EcommerceAPI.Repository.EntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Repository.ManagerRepository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IProductRepository> _productRepository;
        private readonly Lazy<IProductFileRepository> _productFileRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(repositoryContext));
            _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(repositoryContext));
            _productFileRepository = new Lazy<IProductFileRepository>(() => new ProductFileRepository(repositoryContext));
        }
        public IUserRepository User => _userRepository.Value;
        public IProductRepository Product => _productRepository.Value;
        public IProductFileRepository ProductFile => _productFileRepository.Value;
        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
