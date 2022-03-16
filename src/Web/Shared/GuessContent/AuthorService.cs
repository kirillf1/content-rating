using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json;

namespace Web.Shared.GuessContent
{
    public record Author(int Id, string Name);
    public class AuthorService
    {
        private readonly HttpClient client;

        public AuthorService(HttpClient client)
        {
            this.client = client;
        }
       public async Task<List<Author>?> GetAllAuthors()
       {
         return await client.GetFromJsonAsync<List<Author>>("/api/authors");
       }
        public async Task<Author?> AddAuthor(string name)
        {
            var result = await client.PostAsJsonAsync<Author>("/api/authors", new Author(0, name));
            if (result.IsSuccessStatusCode)
                return JsonSerializer.Deserialize<Author>( await result.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
            return default;
        }
    }
}
