using EcommerceAPI.Entity.Models;
using EcommerceAPI.Interface.IRepository.IEntitiesRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Repository.EntityRepository
{
    public class ProductFileRepository : RepositoryBase<ProductFile>, IProductFileRepository
    {
        public ProductFileRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateProductFile(ProductFile productFile)
        {
            productFile.CreatedBy = string.IsNullOrEmpty(productFile.CreatedBy) ? "Unknown" : productFile.CreatedBy;
            productFile.UpdatedBy = string.IsNullOrEmpty(productFile.UpdatedBy) ? "Unknown" : productFile.UpdatedBy;
            Create(productFile);
        }

        public void DeleteProductFile(ProductFile productFile)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductFile> FindProductFileNameAsync(string fileName, bool trackChanges)
        {
            return await FindByCondition(c => c.FileName.Equals(fileName), trackChanges).FirstOrDefaultAsync();
        }
    }
}
