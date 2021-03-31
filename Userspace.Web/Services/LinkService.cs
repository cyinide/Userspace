using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Userspace.Web.Interfaces;
using Userspace.Web.Models;
using Userspace.Web.Resources;

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
        [HttpGet]
        public async Task<IEnumerable<LinkViewModel>> GetLinks(string userId)
        {
            try
            {
                List<LinkViewModel> links = new List<LinkViewModel>();
                if (!String.IsNullOrEmpty(userId))
                {
                    var response = await _httpClient.GetAsync(linksUrl + "/withtagsbyuserid/" + userId);
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    links = JsonConvert.DeserializeObject<List<LinkViewModel>>(apiResponse);
                }
                    return links;
                }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        public async Task<LinkResource> CheckLinkForOccurance(string name)
        {
            try
            {
                var querystring = Uri.EscapeDataString(name);

                var response = await _httpClient.GetAsync(linksUrl + "/checkforoccurance/" + querystring);
                string apiResponse = await response.Content.ReadAsStringAsync();
                var link = JsonConvert.DeserializeObject<LinkResource>(apiResponse);
                return link;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        public async Task<LinkResource> CreateLink(LinkResource link) //linkresource -> linkviewmodel
        {
            try
            {
                var obj = JsonConvert.SerializeObject(link);
                var stringContent = new StringContent(obj, UnicodeEncoding.UTF8, MediaTypeNames.Application.Json);

                var response = await _httpClient.PostAsync(linksUrl, stringContent);

                string apiResponse = await response.Content.ReadAsStringAsync();
                var createdLink = JsonConvert.DeserializeObject<LinkResource>(apiResponse);

                return createdLink;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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

