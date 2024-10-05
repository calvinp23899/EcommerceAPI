using EcommerceAPI.Entity.DTOs;
using EcommerceAPI.Entity.DTOs.OrderDtos;
using EcommerceAPI.Entity.ErrorModels;
using EcommerceAPI.Entity.ResponseModels;
using EcommerceAPI.Interface.IService;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Presentation.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiversion}/order")]
    [ApiController]
    public class OrderControllers : ControllerBase
    {
        private readonly IServiceManager _service;

        public OrderControllers(IServiceManager service)
        {
            _service = service;
        }

        /// <summary>
        /// Perform an action create order.
        /// </summary>
        /// <remarks>
        /// Implementation Note:
        /// - Payment methods support 'COD', 'STRIPE'
        /// - Anonymous fields are come together
        /// </remarks>
        /// <returns>
        /// Create shopping order.
        /// </returns>
        [HttpPost(Name = "V1CreateOrder")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Create order successful.", typeof(ResponseDetails))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal error occurred while perform get all user.", typeof(ErrorDetails))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad request.", typeof(ErrorDetails))]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreationDto request)
        {
            var newOrderDto = await _service.OrderService.CreateOrderAsync(request);
            return Ok(newOrderDto);
        }
    }
}
