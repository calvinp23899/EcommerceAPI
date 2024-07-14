using EcommerceAPI.Entity.DTOs;
using EcommerceAPI.Entity.DTOs.ProductDtos;
using EcommerceAPI.Entity.PaginationModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Interface.IService.IEntityService
{
    public interface IProductService
    {
        Task<(IEnumerable<ProductDto>, int)> GetAllProductsAsync(PaginationParams request, bool trackChanges);
        Task<ProductDto> CreateProductAsync(string product, IFormFile[] Images);
    }
}
