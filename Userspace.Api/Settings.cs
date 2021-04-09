using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Userspace.Api
{
    public static class Settings
    {
        public static string userId = "";
    }
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Secret { get; set; }
        public int ExpirationInDays { get; set; }
    }
}
