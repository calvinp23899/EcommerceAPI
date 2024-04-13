using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Entity.Exceptions
{
    public sealed class RefreshTokenBadRequestException : BadRequestException
    {
        public RefreshTokenBadRequestException(string message): base(message)
        {
        }
    }
}
