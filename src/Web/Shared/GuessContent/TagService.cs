using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
namespace Web.Shared.GuessContent
{
    public class TagService
    {
        private readonly HttpClient httpClient;

        public TagService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<bool> UpdateTag(Tag tag) 
        {
            HttpResponseMessage httpResponseMessage;
            if (tag.Id > 0)
                httpResponseMessage = await httpClient.PutAsJsonAsync("/api/tag/" + tag.Id, tag);
            else
                httpResponseMessage = await httpClient.PostAsJsonAsync("/api/tag", tag);
            return httpResponseMessage.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteTag(int id)
        {
            var response = await httpClient.DeleteAsync("/tag/" + id);
            return response.IsSuccessStatusCode;
        }
        public async Task<List<Tag>?> GetAllTags()
        {
           var tags = await httpClient.GetFromJsonAsync<List<Tag>>("/api/tag");
            return tags;
        }
        public async Task<Tag?> GetTag(int id)
        {
            return await httpClient.GetFromJsonAsync<Tag>("/api/tag/"+id);
        }
    }
}
