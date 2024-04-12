using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Entity.AppConstants
{
    public class AppConstant
    {
        public class JwtKey
        {
            public const string UserId = "userId";
        }

        public class Error
        {
            //DS001-100 For User
            public const string DS001 = "The user with id: {0} is not exist in the database.";
            public const string DS002 = "The user with username: {0} is not exist in the database.";
            public const string DS003 = "Unable to get user due to password is invalid. Please try again.";
        }
    }
}
