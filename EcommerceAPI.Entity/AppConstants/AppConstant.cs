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
            public const string UserId = "";
        }

        public class Error
        {
            //DS001-050 For User
            public const string DS001 = "The user with id: {0} is not exist in the database.";
            public const string DS002 = "The user with username: {0} is not exist in the database.";
            
            //DS050-060 For Authentication
            public const string DS050 = "Unable to get refresh token due to AccessToken is invalid. Please try again.";
            public const string DS051 = "Unable to get refresh token due to client request has some invalid values. Please try again.";
            public const string DS052 = "Unable to get user due to password is invalid. Please try again.";
            public const string DS053 = "Invalid client request due to username or password. Please try again.";
            public const string DS054 = "Authorization Failed. Token is invalid or expired.";
            public const string DS055 = "Token is invalid. Please try again.";

            //DS100-150 Common
            public const string DS100 = "Page number must be a positive integer, equal to 1 and less than or equal to {0}.";
            public const string DS101 = "{0} is not specified.";
            public const string DS102 = "Page size must be a positive integer, greater than 0 and less than or equal to 1 million.";
        }

        public class Logger
        {
            public const string MS001 = "User Controller: Get all user";
        }
    }
}
