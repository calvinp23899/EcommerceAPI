using EcommerceAPI.Interface;
using EcommerceAPI.Interface.IService.IEntityService;
using EcommerceAPI.Interface.IRepository;
using EcommerceAPI.Entity.Models;
using AutoMapper;
using EcommerceAPI.Entity.DTOs;
using EcommerceAPI.Entity.Exceptions;
using System.ComponentModel.Design;
using static EcommerceAPI.Entity.AppConstants.AppConstant;
using EcommerceAPI.Entity.PaginationModels;
using EcommerceAPI.Utils.Common;
using EcommerceAPI.Utils.Validation;


namespace EcommerceAPI.Service.EntityService
{
    internal sealed class UserService : IUserService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private ValidateResourceV1 _validateResourceV1;

        public UserService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _validateResourceV1 = new ValidateResourceV1();
        }
        public async Task<(IEnumerable<UserDto>,int)> GetAllUsersAsync(PaginationParams request, bool trackChanges)
        {
            _logger.LogInfo(Logger.MS001);
            PagingUtils.ValidatePaging(request.PageNumber, request.PageSize);
            var listUser = await _repository.User.GetAllUsersAsync(request, trackChanges);
            var count = await _repository.User.CountAllUserAsync(trackChanges);
            var result = _mapper.Map<IEnumerable<UserDto>>(listUser);
            return (result, count);
        }

        public async Task<UserDto> GetUserAsync(int Id, bool trackChanges)
        {
            var user = await _repository.User.GetUserAsync(Id, trackChanges);
            if (user is null)
                throw new DataNotFoundException(string.Format(Error.DS001,Id));
            var result = _mapper.Map<UserDto>(user);
            return result;
        }

        public async Task<UserDto> CreateUserAsync(UserCreationDto user)
        {
            _validateResourceV1.ValidateUser(ref user);
            var userEntity = _mapper.Map<User>(user);
            _repository.User.CreateUser(userEntity);
            await _repository.SaveAsync();
            var result = _mapper.Map<UserDto>(userEntity);
            return result;
        }
        public async Task UpdateUserAsync(int Id,UserUpdateDto user, bool trackChanges)
        {
            var userEntity = await CheckIfUserExists(Id, trackChanges);
            _mapper.Map(user, userEntity);
            await _repository.SaveAsync();
        }

        public async Task DeleteUserAsync(int Id, UserDeleteDto user, bool trackChanges)
        {
            var userEntity = await CheckIfUserExists(Id, trackChanges);
            _mapper.Map(user, userEntity);
            await _repository.SaveAsync();
        }

        private async Task<User> CheckIfUserExists(int Id, bool trackChanges)
        {
            var user = await _repository.User.GetUserAsync(Id, trackChanges);
            if (user is null)
                throw new DataNotFoundException(string.Format(Error.DS001, Id));
            return user;
        }

        private async Task CheckIfUserNameExists(int Id, bool trackChanges)
        {
            var user = await _repository.User.GetUserAsync(Id, trackChanges);
            if (user is null)
                throw new DataNotFoundException(string.Format(Error.DS001, Id));
        }
    }
}
