using AutoMapper;
using EcommerceAPI.Entity.DTOs;
using EcommerceAPI.Entity.DTOs.ProductDtos;
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
            CreateMap<UserCreationDto, User>()
                //Config date only
                .ForMember(x => x.DateOfBirth, 
                opt => opt.MapFrom(src => src.DateOfBirth.ToDateTime(TimeOnly.MinValue)));
            //Update
            CreateMap<UserUpdateDto, User>();
            //Delete
            CreateMap<UserDeleteDto, User>();
            CreateMap<User, AuthenticationResponseDto>();
            CreateMap<UpdateRefreshTokenDto, User>();

            //Create ProductAPI
            CreateMap<ProductCreationDto, Product>();
            CreateMap<Product, ProductDto>()
            .ForMember(dto => dto.ProductName, opt => opt.MapFrom(p => p.ProductName))
            .ForMember(dto => dto.Price, opt => opt.MapFrom(p => p.Price))
            .ForMember(dto => dto.Quantity, opt => opt.MapFrom(p => p.Quantity))
            .ForMember(dto => dto.Description, opt => opt.MapFrom(p => p.Description))
            .ForMember(dto => dto.Path, opt => opt.MapFrom(src => src.ProductFiles.Select(pf => pf.FilePath).ToList()));

       
        }
    }
}
