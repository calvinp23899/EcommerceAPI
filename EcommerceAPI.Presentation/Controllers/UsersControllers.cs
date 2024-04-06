using EcommerceAPI.Interface.IService;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Presentation.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersControllers : ControllerBase
    {
        private readonly IServiceManager _service;
        public UsersControllers(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var result = await _service.UserService.GetAllUsers(trackChanges: false);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
