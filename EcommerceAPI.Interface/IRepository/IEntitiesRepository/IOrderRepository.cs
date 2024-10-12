using EcommerceAPI.Entity.Models;
using EcommerceAPI.Entity.PaginationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Interface.IRepository.IEntitiesRepository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync(PaginationParams request, bool trackChanges);
        void CreateOrder(Order request);
        Task<int> CountAllOrderAsync(bool trackChanges);
        Task<Order> FindOrderByIdAsync(int Id, bool trackChanges);
        Task DeleteOrderAsync(Order request);



    }
}
