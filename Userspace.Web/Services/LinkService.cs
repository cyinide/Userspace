using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Userspace.Web.Interfaces;
using Userspace.Web.Models;

namespace Userspace.Web.Services
{
    public class LinkService : ILinkService
    {
        public HttpClient _httpClient { get; set; }
        const string linksUrl = "https://localhost:44331/api/Links";

        public LinkService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", Settings.JwtToken);
        }
        public async Task<IEnumerable<LinkViewModel>> GetLinks(string userId)
        {
            try
            {
                List<LinkViewModel> links = new List<LinkViewModel>();
                var response = await _httpClient.GetAsync(linksUrl + "/withtagsbyuserid/" + userId);
                string apiResponse = await response.Content.ReadAsStringAsync();
                links = JsonConvert.DeserializeObject<List<LinkViewModel>>(apiResponse);
                return links;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public Task<LinkViewModel> GetLinkById(int id)
        {
            throw new NotImplementedException();
        }
        public Task<LinkViewModel> CreateLink(LinkViewModel link)
        {
            throw new NotImplementedException();
        }
    }
}
