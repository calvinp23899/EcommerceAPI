using EcommerceAPI.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Entity.Models
{
    public class Order : BaseEntity
    {
        public string OrderNumber { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal? Tax { get; set; }
        public decimal TotalOrder { get; set; }
        public OrderStatus Status { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
