using EcommerceAPI.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Entity.DTOs
{
    public record UserCreationDto(
        string UserName, 
        string Password, 
        string FirstName, 
        string LastName, 
        string Email, 
        string Address, 
        string PhoneNumber, 
        DateOnly DateOfBirth, 
        Role Role
    );
}
