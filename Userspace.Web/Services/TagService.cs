using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Userspace.Web.Interfaces;
using Userspace.Web.Models;
using Userspace.Web.Resources;

namespace Userspace.Web.Services
{
    public class TagService : ITagService
    {
        public HttpClient _httpClient { get; set; }
        const string tagsUrl = "https://localhost:44331/api/Tags";

        public TagService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", Settings.JwtToken);
        }
        public async Task<IEnumerable<TagViewModel>> GetTags()
        {
            try
            {
                List<TagViewModel> tags = new List<TagViewModel>();

                var response = await _httpClient.GetAsync(tagsUrl);
                string apiResponse = await response.Content.ReadAsStringAsync();

                tags = JsonConvert.DeserializeObject<List<TagViewModel>>(apiResponse);
                return tags;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public async Task<IEnumerable<TagResource>> GetTagsByLinkId(int linkId) //tagresource -> tagviewmodel
        {
            try
            {
                List<TagResource> tags = new List<TagResource>();

                var response = await _httpClient.GetAsync(tagsUrl+ "/bylinkid/" + linkId);
                string apiResponse = await response.Content.ReadAsStringAsync();

                tags = JsonConvert.DeserializeObject<List<TagResource>>(apiResponse);
                return tags;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public Task<TagViewModel> GetTagById(int id)
        {
            throw new NotImplementedException();
        }
        public Task<TagViewModel> CreateTag(TagViewModel link)
        {
            throw new NotImplementedException();
        }
    }
}
