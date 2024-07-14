using EcommerceAPI.Entity.Models;
using EcommerceAPI.Entity.PaginationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Interface.IRepository.IEntitiesRepository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(PaginationParams request, bool trackChanges);
        void CreateProduct(Product product, string userName,List<ProductFile> listFiles);
        Task<Product> FindProductNameAsync(string productName, bool trackChanges);
    }
}
