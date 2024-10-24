using EcommerceAPI.Entity.DTOs;
using EcommerceAPI.Entity.DTOs.ProductDtos;
using EcommerceAPI.Entity.ErrorModels;
using EcommerceAPI.Entity.PaginationModels;
using EcommerceAPI.Interface.IService;
using EcommerceAPI.Utils.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace EcommerceAPI.Presentation.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiversion}/products")]
    [ApiController]
    public class ProductsControllers : CustomBaseController
    {
        private readonly IServiceManager _service;
        private readonly IUriService _uriService;

        public ProductsControllers(IServiceManager service, IUriService uriService)
        {
            _service = service;
            _uriService = uriService;
        }

        /// <summary>
        /// Perform an action get all product.
        /// </summary>
        /// <returns>
        /// Added all product.
        /// </returns>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "Get All Products.", typeof(PagedResponse<ProductDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal error occurred while perform get all user.", typeof(ErrorDetails))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad request.", typeof(ErrorDetails))]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetAllProducts([FromQuery] PaginationParams request)
        {
            var route = GetRoute();
            var result = await _service.ProductService.GetAllProductsAsync(request, trackChanges: false);
            var pagedReponse = PaginationHelper.CreatePagedReponse<ProductDto>(result.Item1, request, result.Item2, _uriService, route);
            return Ok(pagedReponse);
        }

        /// <summary>
        /// Perform an action add new product.
        /// </summary>
        /// <remarks>
        /// Implementation Note:
        /// This endpoint is to create a new product.
        /// - Example JsonProduct format in swagger [{"ProductName":"Required String","Price":"Required Decimal","Quantity":"Required Int","Description":"CanBeNull","IsActive":"True/False","IsHot":"True/False"}]
        /// - Example JsonProduct format in form-data postman {"ProductName":"Required String","Price":"Required Decimal","Quantity":"Required Int","Description":"CanBeNull","IsActive":"True/False","IsHot":"True/False"}
        /// - support multiple upload for Images
        /// </remarks>
        /// <returns>
        /// Added new product.
        /// </returns>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, "Create new product.", typeof(ProductDto))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal error occurred while perform get all product.", typeof(ErrorDetails))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad request.", typeof(ErrorDetails))]
        [MapToApiVersion("1.0")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> AddNewProducts([FromForm] string JsonProduct, IFormFile[] Images)
        {
            ProductDto rs = await _service.ProductService.CreateProductAsync(JsonProduct, Images);
            return Ok(rs);
        }

        /// <summary>
        /// Perform an action update existed product by id.
        /// </summary>
        /// <returns>
        /// update a existed product.
        /// </returns>
        [HttpPut("{Id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "update existed product by id.", typeof(ProductDto))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal error occurred while perform get all user.", typeof(ErrorDetails))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad request.", typeof(ErrorDetails))]
        [MapToApiVersion("1.0")]
        [Authorize(Policy = "IsAdmin")]

        public async Task<IActionResult> UpdateProduct(int Id, [FromBody] ProductUpdateDto request)
        {
            await _service.ProductService.UpdateProductAsync(Id, request, true);
            return CustomJsonResponse((int)HttpStatusCode.OK, $"Update successful product id {Id}");
        }

        /// <summary>
        /// Perform an action delete existed product by id.
        /// </summary>
        /// <returns>
        /// Delete a existed product.
        /// </returns>
        [HttpDelete("{Id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Delete existed product by id.", typeof(ErrorDetails))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal error occurred while perform get all user.", typeof(ErrorDetails))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad request.", typeof(ErrorDetails))]
        [MapToApiVersion("1.0")]
        [Authorize(Policy = "IsAdmin")]

        public async Task<IActionResult> DeleteProduct(int Id)
        {
            await _service.ProductService.DeleteProductAsync(Id, true);
            return CustomJsonResponse((int)HttpStatusCode.OK, $"Update successful product id {Id}");
        }
    }
}
