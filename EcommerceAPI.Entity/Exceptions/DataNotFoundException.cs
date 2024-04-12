using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Entity.Exceptions
{
    public sealed class DataNotFoundException : NotFoundException
    {
        public DataNotFoundException(string message) : base(message)
        {
        }
    }
}
