using EcommerceAPI.Entity.Models;
using EcommerceAPI.Entity.PaginationModels;
using EcommerceAPI.Interface.IRepository.IEntitiesRepository;
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

        public void CreateOrder(Order request)
        {
            request.CreatedOn = DateTime.Now;
            request.UpdatedOn = DateTime.Now;
            request.CreatedBy = "Unknown" ;
            request.UpdatedBy = "Unknown";
            request.Status = Entity.Enums.OrderStatus.Prepare;
            request.Tax ??= decimal.Parse("0");
            Create(request);
        }

        public Task<IEnumerable<Order>> GetAllOrdersAsync(PaginationParams request, bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}
