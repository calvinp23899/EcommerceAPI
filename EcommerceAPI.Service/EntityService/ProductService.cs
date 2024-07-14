using AutoMapper;
using EcommerceAPI.Entity.DTOs.ProductDtos;
using EcommerceAPI.Entity.PaginationModels;
using EcommerceAPI.Interface.IRepository;
using EcommerceAPI.Interface;
using EcommerceAPI.Interface.IService.IEntityService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceAPI.Utils.Validation;
using EcommerceAPI.Entity.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using static EcommerceAPI.Entity.AppConstants.AppConstant;
using EcommerceAPI.Entity.Exceptions;
using EcommerceAPI.Entity.DTOs;

namespace EcommerceAPI.Service.EntityService
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private ValidateResourceV1 _validateResourceV1;

        public ProductService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _validateResourceV1 = new ValidateResourceV1();
        }

        public async Task<ProductDto> CreateProductAsync(string jsonProduct, IFormFile[] Images)
        {
            _logger.LogInfo("Create a new product");
            //Handle AddFiles
            List<ProductFile> listFile = new List<ProductFile>();
            if (Images.Length > 0)
            {
                listFile = await _validateResourceV1.ValidateImageListFile(Images);
                listFile.ForEach(n => _repository.ProductFile.CreateProductFile(n));
            }
            //Handle AddProduct
            if (string.IsNullOrWhiteSpace(jsonProduct))
                throw new DataValidationException(Error.DS107);
            ProductCreationDto? request;
            try
            {
                request = JsonConvert.DeserializeObject<ProductCreationDto>(jsonProduct);
            }catch
            {
                throw new DataValidationException(Error.DS105);
            }
            _validateResourceV1.ValidateRequiredProductFields(ref request);
            var ProductExist = await _repository.Product.FindProductNameAsync(request.ProductName, false);
            if (ProductExist != null)
                throw new DataValidationException(string.Format(Error.DS026, request.ProductName));
            var productEntity = _mapper.Map<Product>(request);
            _repository.Product.CreateProduct(productEntity,"Admin", listFile);
            var result = _mapper.Map<ProductDto>(productEntity);

            await _repository.SaveAsync();
            return result;
        }

        public Task<(IEnumerable<ProductDto>, int)> GetAllProductsAsync(PaginationParams request, bool trackChanges)
        {
            _logger.LogInfo("Get all products");
            throw new NotImplementedException();
        }
    }
}
