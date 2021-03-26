using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Userspace.Web.Interfaces;
using Userspace.Web.Models;

namespace Userspace.Web.Services
{
    public class LinkService : ILinkService
    {
        public HttpClient _httpClient { get; set; }
        const string linksUrl = "https://localhost:44331/api/links";

        public LinkService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        public async Task<IEnumerable<Link>> GetLinks()
        {
            try
            {
                List<Link> links = new List<Link>();

                var response = await _httpClient.GetAsync(linksUrl);
                string apiResponse = await response.Content.ReadAsStringAsync();

                links = JsonConvert.DeserializeObject<List<Link>>(apiResponse);
                return links;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public Task<Link> GetLinkById(int id)
        {
            throw new NotImplementedException();
        }
        public Task<Link> CreateLink(Link link)
        {
            throw new NotImplementedException();
        }
    }
}
