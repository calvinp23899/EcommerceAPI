using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Entity.Exceptions
{
    public class DataValidationException : BadRequestException
    {
        public DataValidationException(string message) : base(message)
        {
        }
    }
}
