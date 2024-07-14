using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Entity.Models
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsHot { get; set; }
        public int? VendorId { get; set; }
        public Vendor? Vendor { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
        public ICollection<ProductFile>? ProductFiles { get; set; }

    }
}
