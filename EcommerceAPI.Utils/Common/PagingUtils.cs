using EcommerceAPI.Entity.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static EcommerceAPI.Entity.AppConstants.AppConstant;

namespace EcommerceAPI.Utils.Common
{
    public static class PagingUtils
    {
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns></returns>
        /// <exception cref="DataValidationException">Page number is not specified.</exception>
        /// <exception cref="DataValidationException">Page number must be a positive integer and greater than 1.</exception>
        /// <exception cref="DataValidationException">Page size is not specified.</exception>
        /// <exception cref="DataValidationException">Page size must be a positive integer and greater than 0.</exception>
        public static void ValidatePaging(int? pageNumber = null, int? pageSize = null)
        {
            StringBuilder str = new StringBuilder();
            str.Append(pageSize == 0 ? "PageNumber," : "");
            str.Append(pageNumber == 0 ? "PageSize," : "");    
            if (str.Length > 0)
            {
                str.Remove(str.Length - 1, 1);
                throw new DataValidationException(string.Format(Error.DS101, str));
            }
            if ((pageNumber.Value < 1) || (pageNumber.Value > int.MaxValue))
                throw new DataValidationException(string.Format(Error.DS100, int.MaxValue));
            if (pageSize.Value <= 0 || pageSize.Value > 1000000)
                throw new DataValidationException(Error.DS102);
        }
    }
}
