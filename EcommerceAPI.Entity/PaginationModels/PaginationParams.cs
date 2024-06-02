using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Entity.PaginationModels
{
    public class PaginationParams
    {
        [Required]
        public int PageNumber { get; set; }
        [Required]
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
