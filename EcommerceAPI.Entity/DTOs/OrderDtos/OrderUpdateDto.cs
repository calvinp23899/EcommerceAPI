using EcommerceAPI.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Entity.DTOs.OrderDtos
{
    public record OrderUpdateDto
    (
        OrderStatus Status,
        decimal? Tax,
        decimal TotalOrder,
        List<OrderItem> Items
    );
}
