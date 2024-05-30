using EcommerceAPI.Entity.DTOs;
using EcommerceAPI.Entity.Enums;
using EcommerceAPI.Entity.ErrorModels;
using EcommerceAPI.Entity.PaginationModels;
using EcommerceAPI.Interface.IService;
using EcommerceAPI.Presentation.ActionFilters;
using EcommerceAPI.Service.UriService;
using EcommerceAPI.Utils.Helpers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace EcommerceAPI.Presentation.Controllers.v1
{
    [Route("api/users")]
    [Authorize(Role.SUPER_ADMIN)]
    [ApiController]
    public class UsersControllers : CustomBaseController
    {
        private readonly IServiceManager _service;
        private readonly IUriService _uriService;

        public UsersControllers(IServiceManager service, IUriService uriService)
        {
            _service = service;
            _uriService = uriService;
        }

        /// <summary>
        /// Get a list of users added, pagination is supported.
        /// </summary>
        /// <returns>
        /// Get a list of users added, pagination is supported.
        /// </returns>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "Get All User.", typeof(PagedResponse<UserDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal error occurred while perform get all user.", typeof(ErrorDetails))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad request.", typeof(ErrorDetails))]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetUsers([FromQuery] PaginationParams request)
        {
            var route = GetRoute();
            var result = await _service.UserService.GetAllUsersAsync(request, trackChanges: false);
            var pagedReponse = PaginationHelper.CreatePagedReponse<UserDto>(result.Item1, request, result.Item2, _uriService, route);
            return Ok(pagedReponse);
        }
        /// <summary>
        /// Perform an action get user detail by id.
        /// </summary>
        /// <returns>
        /// Get existed user by id.
        /// </returns>
        [HttpGet("get-id-user/{id}", Name = "UserById")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Get User By Id.", typeof(UserDto))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal error occurred while perform get all user.", typeof(ErrorDetails))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad request.", typeof(ErrorDetails))]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetUserId(int Id)
        {
            var result = await _service.UserService.GetUserAsync(Id, trackChanges: false);
            return Ok(result);
        }
        /// <summary>
        /// Perform an action add a new user.
        /// </summary>
        /// <returns>
        /// Added a new user.
        /// </returns>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "Add a new user.", typeof(UserDto))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal error occurred while perform get all user.", typeof(ErrorDetails))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad request.", typeof(ErrorDetails))]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreationDto request)
        {
            if (request is null)
                return BadRequest("request is null");
            var result = await _service.UserService.CreateUserAsync(request);
            return CreatedAtRoute("UserById", new { id = result.Id }, result);
        }
        /// <summary>
        /// Perform an action update existed user by id.
        /// </summary>
        /// <returns>
        /// Update existed user.
        /// </returns>
        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Update existed user by id.", typeof(UserDto))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal error occurred while perform get all user.", typeof(ErrorDetails))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad request.", typeof(ErrorDetails))]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> UpdateUser(int Id, [FromBody] UserUpdateDto request)
        {
            await _service.UserService.UpdateUserAsync(Id, request, true);
            return NoContent();
        }
        /// <summary>
        /// Perform an action delete existed user by id.
        /// </summary>
        /// <returns>
        /// Delete a existed user.
        /// </returns>
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Delete existed user by id.", typeof(UserDto))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal error occurred while perform get all user.", typeof(ErrorDetails))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad request.", typeof(ErrorDetails))]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> DeleteUser(int Id, [FromBody] UserDeleteDto request)
        {
            await _service.UserService.DeleteUserAsync(Id, request, true);
            return NoContent();
        }

        
    }
}
