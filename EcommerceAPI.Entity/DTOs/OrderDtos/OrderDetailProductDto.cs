using EcommerceAPI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Entity.DTOs.OrderDtos
{
    public class OrderDetailProductDto
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
