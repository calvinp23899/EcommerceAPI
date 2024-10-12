using EcommerceAPI.Entity.DTOs;
using EcommerceAPI.Entity.DTOs.OrderDtos;
using EcommerceAPI.Entity.ErrorModels;
using EcommerceAPI.Entity.PaginationModels;
using EcommerceAPI.Entity.ResponseModels;
using EcommerceAPI.Interface.IService;
using EcommerceAPI.Service.UriService;
using EcommerceAPI.Utils.Helpers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Presentation.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiversion}/order")]
    [ApiController]
    public class OrderControllers : CustomBaseController
    {
        private readonly IServiceManager _service;
        private readonly IUriService _uriService;


        public OrderControllers(IServiceManager service, IUriService uriService)
        {
            _service = service;
            _uriService = uriService;
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
        [SwaggerResponse((int)HttpStatusCode.OK, "Create order successful.", typeof(OrderDto))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal error occurred while perform create order.", typeof(ErrorDetails))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad request.", typeof(ErrorDetails))]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreationDto request)
        {
            var newOrderDto = await _service.OrderService.CreateOrderAsync(request);
            return Ok(newOrderDto);
        }

        /// <summary>
        /// Perform an action get all orders.
        /// </summary>
        /// <returns>
        /// return list of all orders.
        /// </returns>
        [HttpGet(Name = "V1GetAllOrder")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Get all order successful.", typeof(PagedResponse<OrderDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal error occurred while perform get all order.", typeof(ErrorDetails))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad request.", typeof(ErrorDetails))]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetAllOrders([FromQuery] PaginationParams request)
        {
            var route = GetRoute();
            var result = await _service.OrderService.GetAllOrdersAsync(request, trackChanges: false);
            var pagedReponse = PaginationHelper.CreatePagedReponse<OrderDto>(result.Item1, request, result.Item2, _uriService, route);
            return Ok(pagedReponse);
        }

        /// <summary>
        /// Perform an action get order detail by id.
        /// </summary>
        /// <returns>
        /// return order detail.
        /// </returns>
        [HttpGet("{Id:int}", Name = "V1GetOrderDetailById")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Get order detail successful.", typeof(OrderDetailDto))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal error occurred while perform get order detail.", typeof(ErrorDetails))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad request.", typeof(ErrorDetails))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Not Found.", typeof(ErrorDetails))]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetOrderById([Required] int Id)
        {
            var result = await _service.OrderService.GetOrderDetailAsync(Id, trackChanges: false);
            return Ok(result);
        }
        /// <summary>
        /// Perform an action delete order by id.
        /// </summary>
        /// <returns>
        /// return delete message successfully.
        /// </returns>
        [HttpDelete("{Id:int}", Name = "V1DeleteOrderById")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Delete order successful.", typeof(ResponseDetails))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal error occurred while perform get all user.", typeof(ErrorDetails))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad request.", typeof(ErrorDetails))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Not Found.", typeof(ErrorDetails))]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> DeleteOrderById([Required] int Id)
        {
            await _service.OrderService.DeleteOrderAsync(Id, trackChanges: true);
            return CustomJsonResponse(statusCode: 200, $"Delete order with id = {Id} is successful");
        }
    }
}
