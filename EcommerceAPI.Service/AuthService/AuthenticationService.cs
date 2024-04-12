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

        public AuthenticationService(IRepositoryManager repository,ILoggerManager logger, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
            _jwtSetting = new JwtSetting();
            _configuration.Bind(_jwtSetting.Section, _jwtSetting);

        }
        public async Task<TokenDto> CreateToken(AuthenticationRequestDto userDto)
        {
            var user = await ValidateUser(userDto);
            var signingCredentials = GetSigningCredentials();
            var claims = GetClaimsAsync(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            var refreshToken = GenerateRefreshToken();
            return new TokenDto(AccessToken: accessToken, RefreshToken: refreshToken);
        }

        public Task<TokenDto> RefreshToken(TokenDto tokenDto)
        {
            throw new NotImplementedException();
        }

        private async Task<AuthenticationResponseDto> ValidateUser(AuthenticationRequestDto userDto)
        {
            var user = await _repository.User.FindUserNameAsync(userDto.UserName, false);
            if(user == null)
                throw new DataNotFoundException(string.Format(Error.DS002,userDto.UserName));
            if (!VerifyPassword.Verify(userDto.Password, user.Password))
                throw new DataValidationException(Error.DS003);
            var result = _mapper.Map<AuthenticationResponseDto>(user);
            return result;
        }
        private List<Claim> GetClaimsAsync(AuthenticationResponseDto user)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtKey.UserId, user.Id),
                new Claim(ClaimTypes.Name, string.Concat(user.FirstName," ",user.LastName) ??  string.Empty),
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
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims, int expiredMinutes = 0)
        {
            expiredMinutes = expiredMinutes == 0 ? _jwtSetting.ExpiredMinutes : expiredMinutes;
            var tokenOptions = new JwtSecurityToken
            (
                issuer: _jwtSetting.ValidIssuer,
                audience: _jwtSetting.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expiredMinutes),
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
    }
}
