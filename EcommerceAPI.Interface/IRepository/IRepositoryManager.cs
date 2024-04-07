using EcommerceAPI.Interface.IRepository.IEntitiesRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Interface.IRepository
{
    public interface IRepositoryManager
    {
        IUserRepository User { get; }
        Task SaveAsync();
    }
}
