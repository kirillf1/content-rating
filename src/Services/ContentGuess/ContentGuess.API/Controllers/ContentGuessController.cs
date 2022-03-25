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
        [Route("RawContent")]
        public async Task<IEnumerable<ContentInformation>> GetContentCollection([FromQuery] int? contentCount, bool? needShuffle, int? contentType, [FromQuery] List<int>? tagId,
            string? orderColumn, [FromServices] IRequestHandler<ContentInformationRequest, IEnumerable<ContentInformation>> requestHandler)
        {
            var request = new ContentInformationRequest(contentCount ?? 1000)
            {
                TagIds = tagId,
                ContentType = contentType,
                NeedShuffle = needShuffle,
                OrderColumn = orderColumn
            };
            return await requestHandler.HandleAsync(request, default);

        }
        [HttpGet]
        [Route("ById")]
        public async Task<ActionResult<List<ContentRead>>> GetContentCollection([FromQuery] string? orderColumn, [FromQuery] List<long> contentId,
          [FromServices] IRequestHandler<ContentListQuery, List<ContentRead>> requestHandler)
        {
            
            var request = new ContentListQuery(contentId)
            {
               OrderColumn = orderColumn

            };
            return await requestHandler.HandleAsync(request, default);

        }
    }
}
