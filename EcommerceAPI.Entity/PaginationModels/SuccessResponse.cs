using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Entity.PaginationModels
{
    public class SuccessResponse<T>
    {
        public SuccessResponse(){}
        public SuccessResponse(T data)
        {
            Data = data;
        }
        public T Data { get; set; }
    }
}
