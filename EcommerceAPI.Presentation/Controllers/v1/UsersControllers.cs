using EcommerceAPI.Entity.DTOs;
using EcommerceAPI.Entity.Enums;
using EcommerceAPI.Entity.PaginationModels;
using EcommerceAPI.Interface.IService;
using EcommerceAPI.Presentation.ActionFilters;
using EcommerceAPI.Service.UriService;
using EcommerceAPI.Utils.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Presentation.Controllers.v1
{
    [Route("api/users")]
    [Authorize(Role.SUPER_ADMIN)]
    [ApiController]
    public class UsersControllers : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly IUriService _uriService;
        public UsersControllers(IServiceManager service, IUriService uriService)
        {
            _service = service;
            _uriService = uriService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] PaginationParams request)
        {
            var route = GetRoute();
            var result = await _service.UserService.GetAllUsersAsync(request, trackChanges: false);
            var pagedReponse = PaginationHelper.CreatePagedReponse<UserDto>(result.Item1, request, result.Item2, _uriService, route);
            return Ok(pagedReponse);
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

        private string GetRoute()
        {
            var route = Request.Path.Value;
            return route;
        }
    }
}
