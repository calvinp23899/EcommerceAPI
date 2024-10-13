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
using EcommerceAPI.Utils.Common;

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
            _validateResourceV1.ValidateOrderCreation(ref request);
            //Check validation items 
            foreach (var item in request.Items)
            {
                await ValidateProductItems(item.ProductId, item);
            }
            //Create OrderDetail
            var listOrderDetail = _mapper.Map<List<OrderDetail>>(request.Items);
            listOrderDetail.ForEach(x => _repository.OrderDetail.CreateOrderDetail(x));
            //Create Order
            var orderEntity = _mapper.Map<Order>(request);
            orderEntity.UserId = orderEntity.UserId < 1 ? null : orderEntity.UserId;
            orderEntity.OrderNumber = GenerateOrderNumber();
            orderEntity.OrderDetails = listOrderDetail;
            _repository.Order.CreateOrder(orderEntity);
            await _repository.SaveAsync();
            var result = _mapper.Map<OrderDto>(orderEntity);
            return result;

        }

        public async Task<(IEnumerable<OrderDto>, int)> GetAllOrdersAsync(PaginationParams request, bool trackChanges)
        {
            _logger.LogInfo(Logger.MS021);
            PagingUtils.ValidatePaging(request.PageNumber, request.PageSize);
            var listAllOrder = await _repository.Order.GetAllOrdersAsync(request, trackChanges);
            var count = await _repository.Order.CountAllOrderAsync(trackChanges);
            var result = _mapper.Map<IEnumerable<OrderDto>>(listAllOrder);
            return (result, count);
        }

        public async Task<OrderDetailDto> GetOrderDetailAsync(int Id, bool trackChanges)
        {
            _logger.LogInfo(string.Format(Logger.MS022,Id));
            string clientName = string.Empty;

            var orderEntity = await _repository.Order.FindOrderByIdAsync(Id, trackChanges);
            if (orderEntity == null)
                throw new DataValidationException($"Cannot locate the order with id = {Id}. Please try again");

            var listProductOrderDetail = _mapper.Map<List<OrderDetailProductDto>>(orderEntity.OrderDetails);
            listProductOrderDetail.Select(x => GetProductNameOrderDetail(x.ProductId, x).Result).ToList();
            var result = _mapper.Map<OrderDetailDto>(orderEntity);
            result.listProductDto = listProductOrderDetail;
            if(orderEntity.UserId > 0)
            {
                var userEntity = await _repository.User.GetUserAsync((int)orderEntity.UserId, false);
                clientName = userEntity.FirstName;
            }
            result.ClientName = orderEntity.UserId > 0 ? clientName : orderEntity.AnonymousName;
            return result;
        }
        public async Task DeleteOrderAsync(int Id, bool trackChanges)
        {
            var orderEntity = await _repository.Order.FindOrderByIdAsync(Id, trackChanges);
            if (orderEntity == null)
                throw new DataNotFoundException($"Cannot locate the order = {Id}. Please try again.");
            orderEntity.IsDeleted = true;
            await _repository.SaveAsync();
        }

        public async Task<OrderDetailDto> UpdateOrderAsync(int Id, bool trackChanges, OrderUpdateDto requestUpdateOrder)
        {
            _logger.LogInfo($"Update Order with id = {Id}");
            var orderEntity = await _repository.Order.FindOrderByIdAsync(Id, trackChanges);
            if (orderEntity == null)
                throw new DataNotFoundException($"Cannot locate the order = {Id}. Please try again.");
            _validateResourceV1.ValidateOrderUpdated(requestUpdateOrder);
            //Create new order detail
            foreach ( var item in requestUpdateOrder.Items)
            {
                await ValidateProductItems(item.ProductId, item);
            }
            var newListProductUpdate = _mapper.Map<List<OrderDetail>>(requestUpdateOrder.Items);
            newListProductUpdate.ForEach(x => _repository.OrderDetail.CreateOrderDetail(x));
            //Update order
            orderEntity.UpdatedOn = DateTime.Now;
            orderEntity.Status = requestUpdateOrder.Status;
            orderEntity.TotalOrder = requestUpdateOrder.TotalOrder;
            orderEntity.Tax = requestUpdateOrder.Tax;
            orderEntity.OrderDetails = newListProductUpdate; //when attach newListProductUpdate to orderDetail => EF automatic hard delete old product items
            await _repository.SaveAsync();
            //return result
            var listProductOrderDetail = _mapper.Map<List<OrderDetailProductDto>>(newListProductUpdate);
            var result = _mapper.Map<OrderDetailDto>(orderEntity);
            result.listProductDto = listProductOrderDetail;
            return result;
        }
        #region Private Methods
        private string GenerateOrderNumber()
        {
            string dateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string randomSuffix = new Random().Next(1, 9999).ToString();

            return $"{_prefixOrderNumber}{randomSuffix}{dateTime}";
        }
        private async Task ValidateProductItems(int productId, OrderItem itemRequestCreation)
        {
            var rs = await _repository.Product.FindProductByIdAsync(productId, false);
            if (rs == null)
            {
                throw new DataValidationException($"Invalid product id = {productId}");
            }
            if(rs.Price != itemRequestCreation.UnitPrice)
            {
                throw new DataValidationException($"Invalid unit price for product id = {productId}");
            }
            if (itemRequestCreation.Quantity < 1)
            {
                throw new DataValidationException($"Invalid value quantity for product id = {productId}");
            }
            if (itemRequestCreation.UnitPrice * itemRequestCreation.Quantity != itemRequestCreation.TotalPrice)
            {
                throw new DataValidationException($"Invalid total price for product id = {productId}");
            }
        }
        private async Task<OrderDetailProductDto> GetProductNameOrderDetail(int productId, OrderDetailProductDto orderDetailProductDto)
        {
            var productEntity = await _repository.Product.FindProductByIdAsync(productId, false);
            if (productEntity == null)
                throw new DataValidationException($"Invalid product id = {productId}");
            orderDetailProductDto.ProductName = productEntity.ProductName;
            return orderDetailProductDto;
        }
        #endregion
    }
}
