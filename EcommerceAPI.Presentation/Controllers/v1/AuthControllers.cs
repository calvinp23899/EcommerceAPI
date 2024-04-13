using EcommerceAPI.Entity.DTOs;
using EcommerceAPI.Interface.IService;
using Microsoft.AspNetCore.Mvc;


namespace EcommerceAPI.Presentation.Controllers.v1
{
    [Route("api/login")]
    [ApiController]
    public class AuthControllers : ControllerBase
    {
        private readonly IServiceManager _service;

        public AuthControllers(IServiceManager service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequestDto request, bool isCheckRefresh = false)
        {
            var tokenDto = await _service.AuthenticationService.CreateToken(request, isCheckRefresh);
            return Ok(tokenDto);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
        {
            var tokenDtoToReturn = await _service.AuthenticationService.RefreshToken(tokenDto);
            return Ok(tokenDtoToReturn);
        }
    }
}
