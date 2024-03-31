using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Entity.Models
{
    public class ProductFile : BaseEntity
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
