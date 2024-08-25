using AutoMapper;
using EcommerceAPI.Entity.DTOs.ProductDtos;
using EcommerceAPI.Entity.Exceptions;
using EcommerceAPI.Entity.Models;
using EcommerceAPI.Entity.PaginationModels;
using EcommerceAPI.Interface;
using EcommerceAPI.Interface.IRepository;
using EcommerceAPI.Interface.IService.IEntityService;
using EcommerceAPI.Utils.Common;
using EcommerceAPI.Utils.Validation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;
using static EcommerceAPI.Entity.AppConstants.AppConstant;

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
                await CheckingExistFileName(listFile);
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

            await _repository.SaveAsync();
            var result = _mapper.Map<ProductDto>(productEntity);
            return result;
        }

        public async Task DeleteProductAsync(int Id, bool trackChanges)
        {
            _logger.LogInfo($"Delete products by id {Id}");
            var rs = await CheckIfProductExists(Id, trackChanges);
            _repository.Product.DeleteProduct(rs);
            await _repository.SaveAsync();
        }

        public async Task<(IEnumerable<ProductDto>, int)> GetAllProductsAsync(PaginationParams request, bool trackChanges)
        {
            _logger.LogInfo("Get all products");
            PagingUtils.ValidatePaging(request.PageNumber, request.PageSize);
            var listProduct = await _repository.Product.GetAllProductsAsync(request, trackChanges);          
            var result = _mapper.Map<IEnumerable<ProductDto>>(listProduct);
            return (result, listProduct.Count());
        }

        private async Task CheckingExistFileName(List<ProductFile> listFile)
        {
            StringBuilder str = new StringBuilder();
            foreach (var file in listFile)
            {
                var fileExist = await _repository.ProductFile.FindProductFileNameAsync(file.FileName, false);
                if (fileExist != null)
                {
                    str.Append(fileExist.FileName + ",");
                }
            }
            if (str.Length > 0)
            {
                str.Remove(str.Length - 1, 1);
                throw new DataValidationException($"{str} files already existed");
            }
        }

        private async Task<Product> CheckIfProductExists(int Id, bool trackChanges)
        {
            var productEntity = await _repository.Product.FindProductByIdAsync(Id, trackChanges);
            if (productEntity is null)
                throw new DataNotFoundException(string.Format(Error.DS027, Id));
            return productEntity;
        }

        public async Task UpdateProductAsync(int Id, ProductUpdateDto request, bool trackChanges)
        {
            _logger.LogInfo($"Update product id: {Id}");
            var productEntity = await CheckIfProductExists(Id, trackChanges);
            _mapper.Map(request, productEntity);
            if (productEntity.VendorId == 0)
                productEntity.VendorId = null;
            await _repository.SaveAsync();
        }
    }
}
