using EcommerceAPI.Entity.DTOs;
using EcommerceAPI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Interface.IService.IEntityService
{
    public interface IProductFileService
    {
        Task<ProductFile> CreateProductFileAsync(ProductFile user);
    }
}
