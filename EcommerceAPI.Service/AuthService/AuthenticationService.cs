using AutoMapper;
using EcommerceAPI.Entity.DTOs;
using EcommerceAPI.Entity.Exceptions;
using EcommerceAPI.Entity.JwtModel;
using EcommerceAPI.Entity.Models;
using EcommerceAPI.Interface;
using EcommerceAPI.Interface.IRepository;
using EcommerceAPI.Interface.IService.IEntityService;
using EcommerceAPI.Utils.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static EcommerceAPI.Entity.AppConstants.AppConstant;

namespace EcommerceAPI.Service.AuthService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly JwtSetting _jwtSetting;

        public AuthenticationService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
            _jwtSetting = new JwtSetting();
            _configuration.Bind(_jwtSetting.Section, _jwtSetting);

        }
        public async Task<TokenDto> CreateToken(AuthenticationRequestDto userDto, bool isCheckRefresh)
        {
            var user = await ValidateUser(userDto, isCheckRefresh);
            var signingCredentials = GetSigningCredentials();
            var claims = GetClaimsAsync(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            var refreshToken = GenerateRefreshToken();
            await UpdateRefreshTokenForUser(Convert.ToInt32(user.Id), refreshToken);
            return new TokenDto(accessToken, refreshToken);
        }

        public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
        {
            var validEntity = ValidateJwtToken(tokenDto.AccessToken);
            var userEntity = await _repository.User.GetUserAsync(Convert.ToInt32(validEntity.Id), false);
            if (userEntity == null || userEntity.RefreshToken != tokenDto.RefreshToken ||
            userEntity.RefreshTokenExpiryTime <= DateTime.Now)
                throw new RefreshTokenBadRequestException(Error.DS051);
            var refreshDto = new AuthenticationRequestDto
            {
                UserName = userEntity.Username,
                Password = userEntity.Password,
            };
            return await CreateToken(refreshDto, true);
        }
        private async Task<AuthenticationResponseDto> ValidateUser(AuthenticationRequestDto userDto, bool isCheckRefresh)
        {
            if (userDto.UserName.ToLower().Equals("null") == true ||
                userDto.Password.ToLower().Equals("null") == true ||
                string.IsNullOrWhiteSpace(userDto.UserName) ||
                string.IsNullOrWhiteSpace(userDto.Password)
            )
                throw new DataValidationException(Error.DS053);
            var user = await _repository.User.FindUserNameAsync(userDto.UserName, true);
            if(user == null)
                throw new DataNotFoundException(string.Format(Error.DS002,userDto.UserName));
            if (!isCheckRefresh)
            {
                if (!VerifyPassword.Verify(userDto.Password, user.Password))
                    throw new DataValidationException(Error.DS052);
            }
            var result = _mapper.Map<AuthenticationResponseDto>(user);
            return result;
        }
        private List<Claim> GetClaimsAsync(AuthenticationResponseDto user)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtKey.UserId, user.Id),
                new Claim(ClaimTypes.Name, string.Concat(user.FirstName," ",user.LastName) ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role ?? string.Empty),
            };
            return claims;
        }
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"));
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken
            (
                issuer: _jwtSetting.ValidIssuer,
                audience: _jwtSetting.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSetting.ExpiredMinutes),
                signingCredentials: signingCredentials
            );
            return tokenOptions;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        private async Task UpdateRefreshTokenForUser(int Id, string refreshToken)
        {
            var userEntity = await _repository.User.GetUserAsync(Id, true);
            var userRefreshToken = new UpdateRefreshTokenDto();
            userRefreshToken.RefreshToken = refreshToken;
            userRefreshToken.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            _mapper.Map(userRefreshToken, userEntity);
            await _repository.SaveAsync();
        }
        public AuthenticationResponseDto ValidateJwtToken(string token, bool isExpiredToken = false)
        {
            if(string.IsNullOrWhiteSpace(token) || token == "null")
                throw new RefreshTokenBadRequestException(Error.DS055);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"))),
                ValidateLifetime = isExpiredToken,
                ValidIssuer = _jwtSetting.ValidIssuer,
                ValidAudience = _jwtSetting.ValidAudience,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtToken = (JwtSecurityToken)securityToken;
            return new AuthenticationResponseDto
            {
                Id = int.Parse(jwtToken.Claims.ToList().FirstOrDefault().Value).ToString(),
                Role = jwtToken.Claims.ToList()[2].Value,
            };
        }
    }
}
