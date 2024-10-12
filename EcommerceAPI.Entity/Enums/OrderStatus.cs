using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Entity.Enums
{
    public enum OrderStatus
    {
        PREPARE = 0,
        DELIVERING = 1,
        SUCCESS = 2,
        CANCEL = 3,
    }
}
