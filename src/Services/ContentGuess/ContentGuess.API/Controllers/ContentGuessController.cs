using ContentGuess.Application.ContentHandlers;
using ContentGuess.Application.Dto;
using ContentGuess.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContentGuess.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentGuessController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<ContentForGuess>>> GetContentCollection(int? contentCount,int? falseNames,int? contentTypeId, [FromQuery]List<int>? tagId,
          [FromServices] IRequestHandler<ContentListForGuessQuery, List<ContentForGuess>> requestHandler)
        {
            Console.WriteLine("Names" + falseNames);
            var request = new ContentListForGuessQuery(contentCount ?? 25,falseNames ?? 5)
            {
                TagIds = tagId,
                ContentType = contentTypeId
               
            };
            return await requestHandler.HandleAsync(request, default);

        }
    }
}
