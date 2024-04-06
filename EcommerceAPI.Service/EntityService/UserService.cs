using EcommerceAPI.Interface;
using EcommerceAPI.Interface.IService.IEntityService;
using EcommerceAPI.Interface.IRepository;
using EcommerceAPI.Entity.Models;


namespace EcommerceAPI.Service.EntityService
{
    internal sealed class UserService : IUserService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public UserService(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<IEnumerable<User>> GetAllUsers(bool trackChanges)
        {
            try
            {
                var listUser = await _repository.User.GetAllUsers(trackChanges);
                return listUser;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Can not execute {nameof(GetAllUsers)} in the {nameof(UserService)} method {ex}");
            throw;
            }
        }

    }
}
