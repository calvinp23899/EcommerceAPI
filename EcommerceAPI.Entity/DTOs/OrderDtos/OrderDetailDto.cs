using EcommerceAPI.Entity.DTOs.ProductDtos;
using EcommerceAPI.Entity.Enums;
using EcommerceAPI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Entity.DTOs.OrderDtos
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public OrderStatus Status { get; set; }
        public decimal? Tax { get; set; }
        public decimal TotalOrder { get; set; }
        public string? ClientName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public List<OrderDetailProductDto> listProductDto { get; set; }

    }
}
