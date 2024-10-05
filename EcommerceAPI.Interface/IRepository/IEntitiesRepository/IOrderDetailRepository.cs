using EcommerceAPI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Interface.IRepository.IEntitiesRepository
{
    public interface IOrderDetailRepository
    {
        void CreateOrderDetail(OrderDetail orderDetailItem);
    }
}
