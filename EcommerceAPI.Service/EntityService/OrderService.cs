using AutoMapper;
using EcommerceAPI.Entity.DTOs.OrderDtos;
using EcommerceAPI.Entity.PaginationModels;
using EcommerceAPI.Interface.IRepository;
using EcommerceAPI.Interface;
using EcommerceAPI.Interface.IService.IEntityService;
using EcommerceAPI.Utils.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcommerceAPI.Entity.AppConstants.AppConstant;
using EcommerceAPI.Entity.DTOs;
using EcommerceAPI.Entity.Models;
using EcommerceAPI.Entity.Exceptions;

namespace EcommerceAPI.Service.EntityService
{
    public class OrderService : IOrderService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private ValidateResourceV1 _validateResourceV1;
        private const string _prefixOrderNumber = "No";

        public OrderService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _validateResourceV1 = new ValidateResourceV1();
        }

        public async Task<OrderDto> CreateOrderAsync(OrderCreationDto request)
        {
            _logger.LogInfo($"Create Order");
            _validateResourceV1.ValidateRequiredOrderFields(ref request);
            //Check exists items
            foreach(var item in request.Items)
            {
                await CheckExistProduct(item.ProductId);
            }
            //Create OrderDetail
            var listOrderDetail = _mapper.Map<List<OrderDetail>>(request.Items);
            listOrderDetail.ForEach(x => _repository.OrderDetail.CreateOrderDetail(x));
            //Create Order
            var orderEntity = _mapper.Map<Order>(request);
            orderEntity.OrderNumber = GenerateOrderNumber();
            orderEntity.OrderDetails = listOrderDetail;
            _repository.Order.CreateOrder(orderEntity);
            await _repository.SaveAsync();
            var result = _mapper.Map<OrderDto>(orderEntity);
            return result;

        }

        public Task<(IEnumerable<OrderDto>, int)> GetAllOrdersAsync(PaginationParams request, bool trackChanges)
        {
            throw new NotImplementedException();
        }
        private string GenerateOrderNumber()
        {
            string dateTime = DateTime.Now.ToString("yyyyMMddHHmmss");  
            string randomSuffix = new Random().Next(1, 9999).ToString(); 

            return $"{_prefixOrderNumber}{randomSuffix}{dateTime}";
        }
        private async Task CheckExistProduct(int productId)
        {
            var rs = await _repository.Product.FindProductByIdAsync(productId, false);
            if(rs == null)
            {
                throw new DataValidationException($"Invalid product id = {productId}");
            }
        }
    }
}
