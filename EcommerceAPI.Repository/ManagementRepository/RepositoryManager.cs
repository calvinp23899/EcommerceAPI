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
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _userRepository = new Lazy<IUserRepository>(() => new
            UserRepository(repositoryContext));
        }
        public IUserRepository User => _userRepository.Value;
        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();

    }
}
