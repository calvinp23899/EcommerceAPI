using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Entity.Models
{
    public class Vendor : BaseEntity
    {
        public string VendorName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
