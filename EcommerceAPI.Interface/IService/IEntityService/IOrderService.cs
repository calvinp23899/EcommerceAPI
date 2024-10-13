using EcommerceAPI.Entity.DTOs;
using EcommerceAPI.Entity.DTOs.OrderDtos;
using EcommerceAPI.Entity.DTOs.ProductDtos;
using EcommerceAPI.Entity.PaginationModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Interface.IService.IEntityService
{
    public interface IOrderService
    {
        Task<(IEnumerable<OrderDto>, int)> GetAllOrdersAsync(PaginationParams request, bool trackChanges);
        Task<OrderDto> CreateOrderAsync(OrderCreationDto request);
        Task<OrderDetailDto> GetOrderDetailAsync(int Id, bool trackChanges);
        Task DeleteOrderAsync(int Id, bool trackChanges);
        Task<OrderDetailDto> UpdateOrderAsync(int Id, bool trackChanges, OrderUpdateDto requestUpdateOrder);

    }
}
