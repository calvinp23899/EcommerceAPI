using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Entity.DTOs
{
    public record UserDto(int Id, string FullName, string Email, string PhoneNumber, string Address);
}
