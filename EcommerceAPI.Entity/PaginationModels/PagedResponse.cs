using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Entity.PaginationModels
{
    public class PagedResponse<T> : SuccessResponse<T>
    {
        [JsonProperty(Order = 1)]
        public int PageNumber { get; set; }
        [JsonProperty(Order = 2)]
        public int PageSize { get; set; }
        [JsonProperty(Order = 3)]
        public Uri FirstPage { get; set; }
        [JsonProperty(Order = 4)]
        public Uri LastPage { get; set; }
        [JsonProperty(Order = 5)]
        public int TotalPages { get; set; }
        [JsonProperty(Order = 6)]
        public int TotalRecords { get; set; }
        [JsonProperty(Order = 7)]
        public Uri NextPage { get; set; }
        [JsonProperty(Order = 8)]
        public Uri PreviousPage { get; set; }
        public PagedResponse(T data, int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
        }
    }
}
