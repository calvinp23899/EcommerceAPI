using EcommerceAPI.Entity.DTOs.ProductDtos;
using EcommerceAPI.Entity.ErrorModels;
using EcommerceAPI.Entity.PaginationModels;
using EcommerceAPI.Interface.IService;
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

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "Get All Products.", typeof(PagedResponse<ProductDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal error occurred while perform get all user.", typeof(ErrorDetails))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad request.", typeof(ErrorDetails))]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetAllProducts([FromQuery] PaginationParams request)
        {
            return Ok(request);
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
        public async Task<IActionResult> AddNewProducts([FromForm] string JsonProduct, IFormFile[] Images)
        {
            ProductDto rs = await _service.ProductService.CreateProductAsync(JsonProduct, Images);
            return Ok(rs);
        }
    }
}
