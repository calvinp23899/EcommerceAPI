using EcommerceAPI.Entity.DTOs;
using EcommerceAPI.Entity.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcommerceAPI.Entity.AppConstants.AppConstant;

namespace EcommerceAPI.Utils.Validation
{
    public class ValidateResourceV1
    {
        public ValidateResourceV1()
        {
        }
        virtual public void ValidateUser(ref UserCreationDto user)
        {
            StringBuilder str = new StringBuilder();
            if (String.IsNullOrWhiteSpace(user.UserName))
                str.Append($"{nameof(user.UserName)},");
            if (String.IsNullOrWhiteSpace(user.Password))
                str.Append($"{nameof(user.Password)},");
            if (String.IsNullOrWhiteSpace(user.FirstName))
                str.Append($"{nameof(user.FirstName)},");
            if (String.IsNullOrWhiteSpace(user.LastName))
                str.Append($"{nameof(user.LastName)},");
            if (String.IsNullOrWhiteSpace(user.Email))
                str.Append($"{nameof(user.Email)},");
            if (String.IsNullOrWhiteSpace(user.PhoneNumber))
                str.Append($"{nameof(user.PhoneNumber)},");
            if (str.Length > 0)
            {
                str.Length--;
                throw new DataValidationException(string.Format(Error.DS103, str));
            }               
            return;
        }
    }
}
