using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Userspace.Web
{
    public static class Settings
    {
        public static string CurrentUserName = "";
        public static string CurrentUserId = "";
        public static string JwtToken = "";
        public static string ErrorMessage = "User cannot add same link multiple times. " +
                            "Each link - tag relation must have a value. " +
                            "Link must have at least one tag associated with it. ";
    }
    public class ApiEndpoint
    {
        public string AuthEndpointUrl { get; set; }
        public string LinksEndpointUrl { get; set; }
        public string TagsEndpointUrl { get; set; }
    }
}
