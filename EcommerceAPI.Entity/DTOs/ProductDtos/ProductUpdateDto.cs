using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Entity.DTOs.ProductDtos
{
    public record ProductUpdateDto(string ProductName, decimal Price, int Quantity, string? Description, bool? IsActive, bool? IsHot, int? VendorId = null);
}
