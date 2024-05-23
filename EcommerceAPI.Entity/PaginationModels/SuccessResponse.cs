using Newtonsoft.Json;
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
        [JsonProperty(Order = 9)]
        public T Data { get; set; }
    }
}
