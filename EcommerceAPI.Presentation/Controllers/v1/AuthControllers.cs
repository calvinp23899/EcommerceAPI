using EcommerceAPI.Entity.DTOs;
using EcommerceAPI.Entity.ErrorModels;
using EcommerceAPI.Entity.PaginationModels;
using EcommerceAPI.Interface.IService;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;


namespace EcommerceAPI.Presentation.Controllers.v1
{
    [Route("api/authen")]
    [ApiController]
    public class AuthControllers : ControllerBase
    {
        private readonly IServiceManager _service;

        public AuthControllers(IServiceManager service)
        {
            _service = service;
        }

        /// <summary>
        /// Perform an action login, for user's authentication.
        /// </summary>
        /// <returns>
        /// Get accesstoken and refresh token.
        /// </returns>
        [HttpPost("login")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Login Authentication.", typeof(TokenDto))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal error occurred while perform get all user.", typeof(ErrorDetails))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad request.", typeof(ErrorDetails))]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequestDto request)
        {
            var tokenDto = await _service.AuthenticationService.CreateToken(request, request.isCheckRefresh);
            return Ok(tokenDto);
        }

        /// <summary>
        /// Perform an action refresh token.
        /// </summary>
        /// <returns>
        /// Get refresh token.
        /// </returns>
        [HttpPost("refresh-token")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Login Authentication.", typeof(TokenDto))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal error occurred while perform get all user.", typeof(ErrorDetails))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad request.", typeof(ErrorDetails))]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
        {
            var tokenDtoToReturn = await _service.AuthenticationService.RefreshToken(tokenDto);
            return Ok(tokenDtoToReturn);
        }
    }
}
