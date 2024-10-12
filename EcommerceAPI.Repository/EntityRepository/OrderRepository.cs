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
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<int> CountAllOrderAsync(bool trackChanges)
        {
            return await FindByCondition(c => c.IsDeleted == false, trackChanges).CountAsync();
        }

        public void CreateOrder(Order request)
        {
            request.CreatedOn = DateTime.Now;
            request.UpdatedOn = DateTime.Now;
            request.CreatedBy = "Unknown" ;
            request.UpdatedBy = "Unknown";
            request.Status = Entity.Enums.OrderStatus.PREPARE;
            request.Tax ??= decimal.Parse("0");
            request.IsDeleted = false;
            Create(request);
        }

        public Task DeleteOrderAsync(Order request)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> FindOrderByIdAsync(int Id, bool trackChanges)
        {
            #pragma warning disable CS8603 // Possible null reference return.
            return await FindByCondition(c => c.Id.Equals(Id), trackChanges).Include(x=>x.OrderDetails).FirstOrDefaultAsync();
            #pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync(PaginationParams request, bool trackChanges)
        {
            return await FindByCondition(c => c.IsDeleted == false, trackChanges)
                  .OrderByDescending(c => c.Id)
                  .Skip((request.PageNumber - 1) * request.PageSize)
                  .Take(request.PageSize)
                  .ToListAsync();
        }
    }
}
