using EcommerceAPI.Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Interface.IService.IEntityService
{
    public interface IAuthenticationService
    {
        Task<TokenDto> CreateToken(AuthenticationRequestDto userDto);
        Task<TokenDto> RefreshToken(TokenDto tokenDto);
    }
}
