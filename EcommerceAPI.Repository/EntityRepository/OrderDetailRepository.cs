using EcommerceAPI.Entity.Models;
using EcommerceAPI.Interface.IRepository.IEntitiesRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Repository.EntityRepository
{
    public class OrderDetailRepository : RepositoryBase<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
        public void CreateOrderDetail(OrderDetail orderDetailItem)
        {
            orderDetailItem.CreatedBy = string.IsNullOrEmpty(orderDetailItem.CreatedBy) ? "Unknown" : orderDetailItem.CreatedBy;
            orderDetailItem.UpdatedBy = string.IsNullOrEmpty(orderDetailItem.UpdatedBy) ? "Unknown" : orderDetailItem.UpdatedBy;
            orderDetailItem.UpdatedOn = DateTime.Now;
            orderDetailItem.CreatedOn = DateTime.Now;
            Create(orderDetailItem);
        }
    }
}
