using EcommerceAPI.Entity.DTOs;
using EcommerceAPI.Interface.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequestDto request)
        {
            var tokenDto = await _service.AuthenticationService.CreateToken(request);
            return Ok(tokenDto);
        }
    }
}
