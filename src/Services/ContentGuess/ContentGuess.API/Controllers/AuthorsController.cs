using ContentGuess.Application.AuthorHandlers;
using ContentGuess.Domain;
using ContentGuess.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContentGuess.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {

        [HttpGet]
        public async Task<List<Author>> Get([FromServices] IRequestHandler<GetAuthorsQueryRequest, List<Author>> handler)
        {
            return await handler.HandleAsync(new GetAuthorsQueryRequest(), default);
        }
        [HttpPost]
        public async Task<Author> Add([FromBody] Author author, [FromServices] IRequestHandler<AddAuthorRequest, Author> handler)
        {
            author = await handler.HandleAsync(new AddAuthorRequest(author), default);
            return author;
        }
        [HttpPut("{id}")]
        public async Task<Author> Update(int id,[FromBody] Author author, [FromServices] IRequestHandler<UpdateAuthorRequest, Author> handler)
        {
            return await handler.HandleAsync(new UpdateAuthorRequest(id, author), default);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id,[FromServices]IContentGuessDbContext contentGuess)
        {
            contentGuess.Author.Remove(new Author("") { Id= id });
            await contentGuess.SaveChangesAsync(default);
            return Ok();
        }
    }
}
