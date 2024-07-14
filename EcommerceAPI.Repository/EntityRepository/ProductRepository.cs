using EcommerceAPI.Entity.Models;
using EcommerceAPI.Entity.PaginationModels;
using EcommerceAPI.Interface.IRepository.IEntitiesRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Repository.EntityRepository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public void CreateProduct(Product product, string userName, List<ProductFile> listFiles)
        {
            product.IsActive = product.IsActive == true ? true : false;
            product.IsHot = product.IsHot == true ? true : false;
            product.CreatedOn = DateTime.Now;
            product.UpdatedOn = DateTime.Now;
            product.CreatedBy = userName == null ? "Unknown" : userName;
            product.UpdatedBy = userName == null ? "Unknown" : userName;
            product.ProductFiles = listFiles;
            Create(product);
        }

        public Task<IEnumerable<Product>> GetAllProductsAsync(PaginationParams request, bool trackChanges)
        {
            throw new NotImplementedException();
        }
        public async Task<Product> FindProductNameAsync(string productName, bool trackChanges)
        {
            return await FindByCondition(c => c.ProductName.Equals(productName.ToLower().Trim()), trackChanges).SingleOrDefaultAsync();
        }
    }
}
