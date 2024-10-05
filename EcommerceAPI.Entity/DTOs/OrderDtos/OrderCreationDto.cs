using EcommerceAPI.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Entity.DTOs.OrderDtos
{
    public record OrderCreationDto
    (
        int UserId,
        string AnonymousName,
        string AnonymousEmail,
        string AnonymousPhone,
        string AnonymousAddress,
        PaymentMethod PaymentMethod,
        decimal TotalOrder,
        List<OrderItem> Items
    );
    public record OrderItem
    (
        int ProductId,
        int Quantity,
        decimal UnitPrice,
        decimal TotalPrice
    );
}
