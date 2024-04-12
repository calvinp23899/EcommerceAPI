using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Entity.JwtModel
{
    public class JwtSetting
    {
        public string Section { get; set; } = "JwtSettings";
        public string ValidIssuer { get; set; } = string.Empty;
        public string ValidAudience { get; set; } = string.Empty;
        public int ExpiredMinutes { get; set; }
    }

}
