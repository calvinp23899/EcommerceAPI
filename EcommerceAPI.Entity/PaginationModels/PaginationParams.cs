using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Entity.PaginationModels
{
    public class PaginationParams
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PaginationParams()
        {}
        public PaginationParams(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }
    }
}
