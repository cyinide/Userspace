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
    public class TagService : ITagService
    {
        public HttpClient _httpClient { get; set; }
        const string tagsUrl = "https://localhost:44331/api/links";

        public TagService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IEnumerable<Tag>> GetTags()
        {
            try
            {
                List<Tag> tags = new List<Tag>();

                var response = await _httpClient.GetAsync(tagsUrl);
                string apiResponse = await response.Content.ReadAsStringAsync();

                tags = JsonConvert.DeserializeObject<List<Tag>>(apiResponse);
                return tags;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public Task<Tag> GetTagById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Tag> CreateTag(Tag link)
        {
            throw new NotImplementedException();
        }
    }
}
