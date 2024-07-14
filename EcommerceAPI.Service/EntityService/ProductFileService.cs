using AutoMapper;
using EcommerceAPI.Interface.IRepository;
using EcommerceAPI.Interface;
using EcommerceAPI.Interface.IService.IEntityService;
using EcommerceAPI.Utils.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceAPI.Entity.Models;

namespace EcommerceAPI.Service.EntityService
{
    public class ProductFileService : IProductFileService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private ValidateResourceV1 _validateResourceV1;
        public ProductFileService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _validateResourceV1 = new ValidateResourceV1();
        }

        public async Task<ProductFile> CreateProductFileAsync(ProductFile productFile)
        {
            _repository.ProductFile.CreateProductFile(productFile);
            await _repository.SaveAsync();
            return productFile;
        }
    }
}
