using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json;

namespace Web.Shared.GuessContent
{
    record ContentGuessWrite(string Name,string Url, int ContentTypeId, List<int> TagIds, int? AuthorId, double? ContentStartSeconds); 
  
    
    public class ContentGuessService
    {
        private readonly HttpClient httpClient;

        public ContentGuessService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<ContentGuessEdit?> GetContentForUpdate(long id)
        {
           return await httpClient.GetFromJsonAsync<ContentGuessEdit>("/api/Content/" + id);
        }
        public async Task<List<ContentGuess>?> GetAllContent()
        {
            return await httpClient.GetFromJsonAsync<List<ContentGuess>>("/api/content?ordercolumn=name");
        }
        public async Task<List<ContentGuessFalseNames>?> GetContentGuess(int count,int falseNameCount,List<int>? selectedTags = null,string? contentType = null)
        {
            var query = "/api/contentguess?";
            query += $"contentCount={count}&falseNames={falseNameCount}&";
            if (contentType != null)
                query += $"contentType={contentType}&";
            if (selectedTags != null)
                selectedTags.ForEach(t => query += "tagId=" + t+"&");
           return await httpClient.GetFromJsonAsync<List<ContentGuessFalseNames>>(query, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        public async Task<bool> UpdateContentGuess(ContentGuessEdit content,long id)
        {
            var contentForUpdate = new ContentGuessWrite(content.Name,content.Url,content.ContentTypeId,content.TagIds!,
                content.AuthorId, content.ContentStartSeconds);
            HttpResponseMessage httpResponseMessage;
            if (id > 0)
                httpResponseMessage = await httpClient.PutAsJsonAsync("/api/content/" + id, contentForUpdate);
            else
                httpResponseMessage = await httpClient.PostAsJsonAsync("/api/content", contentForUpdate);
            return httpResponseMessage.IsSuccessStatusCode;
        }
        public async Task<bool> AddContentCollection(List<ContentGuessEdit> contentCollection)
        {
            var contentAdd = contentCollection.Select(c => new ContentGuessWrite(c.Name, c.Url, c.ContentTypeId,
                c.TagIds!, c.AuthorId, c.ContentStartSeconds));
            var httpResponseMessage = await httpClient.PostAsJsonAsync("/api/content", contentAdd);
            return httpResponseMessage.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteContent(int Id)
        {
           var result = await httpClient.DeleteAsync("/api/content/" + Id);
           return result.IsSuccessStatusCode;
        }
        
    }
}
