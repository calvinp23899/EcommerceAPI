using EcommerceAPI.Entity.PaginationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Interface.IService
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationParams filter, string route);
    }
}
