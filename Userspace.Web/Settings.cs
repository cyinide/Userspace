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
    }
    public class ApiEndpoint
    {
        public string AuthEndpointUrl { get; set; }
        public string LinksEndpointUrl { get; set; }
        public string TagsEndpointUrl { get; set; }
    }
    public class ApiEndpointLocal
    {
        public string AuthEndpointUrl { get; set; }
        public string LinksEndpointUrl { get; set; }
        public string TagsEndpointUrl { get; set; }
    }
}
