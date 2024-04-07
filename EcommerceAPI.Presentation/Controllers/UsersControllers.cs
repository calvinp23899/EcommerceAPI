using EcommerceAPI.Entity.DTOs;
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
            var result = await _service.UserService.GetAllUsersAsync(trackChanges: false);
            return Ok(result);
        }

        [HttpGet("get-id-user/{id}", Name = "UserById")]
        public async Task<IActionResult> GetUserId(int id)
        {
            var result = await _service.UserService.GetUserAsync(id, trackChanges: false);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreationDto request)
        {
            if (request is null)
                return BadRequest("request is null");
            var result = await _service.UserService.CreateUserAsync(request);
            return CreatedAtRoute("UserById", new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int Id, [FromBody] UserUpdateDto request)
        {
            await _service.UserService.UpdateUserAsync(Id, request, true);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int Id, [FromBody] UserDeleteDto request)
        {
            await _service.UserService.DeleteUserAsync(Id, request, true);
            return NoContent();
        }
    }
}
