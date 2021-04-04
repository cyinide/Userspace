using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Userspace.Web.Interfaces;
using Userspace.Web.Models.Auth;

namespace Userspace.Web.Services
{
    public class AuthService : IAuthService
    {
        public HttpClient _httpClient { get; set; }
        private string authUrl;
        public AuthService(IHttpClientFactory httpClientFactory, ApiEndpointLocal apiEndpoint)
        {
            _httpClient = httpClientFactory.CreateClient();
            authUrl = apiEndpoint.AuthEndpointUrl;
        }
        public async Task<bool> Login(LoginViewModel model)
        {
            try
            {
                var obj = JsonConvert.SerializeObject(model);
                var stringContent = new StringContent(obj, UnicodeEncoding.UTF8, MediaTypeNames.Application.Json);
                var result = await _httpClient.PostAsync(authUrl + "/signin", stringContent);
                if (result.IsSuccessStatusCode)
                {
                    var jwt = await result.Content.ReadAsStringAsync();

                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(jwt);
                    Settings.CurrentUserId = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                    Settings.CurrentUserName = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                    Settings.JwtToken = jwt;
                }

                return result.IsSuccessStatusCode;    
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> Register(RegisterViewModel model)
        {
            try
            {
                var obj = JsonConvert.SerializeObject(model);
                var stringContent = new StringContent(obj, UnicodeEncoding.UTF8, MediaTypeNames.Application.Json);
                var result = await _httpClient.PostAsync(authUrl + "/signup", stringContent);

                return result.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
