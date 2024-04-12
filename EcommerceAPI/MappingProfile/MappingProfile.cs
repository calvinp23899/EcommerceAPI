using AutoMapper;
using EcommerceAPI.Entity.DTOs;
using EcommerceAPI.Entity.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EcommerceAPI.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Get
            CreateMap<User, UserDto>()
            .ForCtorParam("FullName",
            opt => opt.MapFrom(x => string.Join(' ', x.FirstName, x.LastName)));
            //Create
            CreateMap<UserCreationDto, User>();
            //Update
            CreateMap<UserUpdateDto, User>();
            //Delete
            CreateMap<UserDeleteDto, User>();
            CreateMap<User, AuthenticationResponseDto>();

        }
    }
}
