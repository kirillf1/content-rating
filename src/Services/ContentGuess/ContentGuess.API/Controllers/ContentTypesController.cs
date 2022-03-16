using ContentGuess.Domain;
using ContentGuess.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContentGuess.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentTypesController : ControllerBase
    {
        private readonly IContentGuessDbContext contentGuessDbContext;

        public ContentTypesController(IContentGuessDbContext contentGuessDbContext)
        {
            this.contentGuessDbContext = contentGuessDbContext;
        }
        [HttpGet]
        public async Task<List<ContentType>> Get()
        {
            return await contentGuessDbContext.ContentType.ToListAsync();
        }
    }
}
