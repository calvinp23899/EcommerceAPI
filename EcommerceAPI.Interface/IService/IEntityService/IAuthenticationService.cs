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
        public AuthenticationResponseDto ValidateJwtToken(string token, bool isExpiredToken);
        Task<TokenDto> CreateToken(AuthenticationRequestDto userDto, bool isCheckRefresh);
        Task<TokenDto> RefreshToken(TokenDto tokenDto);
    }
}
