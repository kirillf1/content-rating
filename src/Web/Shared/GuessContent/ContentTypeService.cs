using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
namespace Web.Shared.GuessContent
{
    public record ContentType(int Id, string Name);
    public class ContentTypeService
    {
        private readonly HttpClient httpClient;

        public ContentTypeService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<List<ContentType>?> GetContentTypes()
        {
            return await httpClient.GetFromJsonAsync<List<ContentType>>("/api/contentTypes");
        }
    }
}
