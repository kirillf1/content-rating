using ContentGuess.Application.ContentHandlers;
using ContentGuess.Application.Dto;
using ContentGuess.Domain;
using ContentGuess.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContentGuess.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ContentWrite>> GetContent([FromRoute] long id, [FromServices] IRequestHandler<GetContentRequest, ContentWrite> requestHandler)
        {
            return await requestHandler.HandleAsync(new GetContentRequest(id), default);
        }
        [HttpGet]
        public async Task<IEnumerable<ContentInformation>> GetContentCollection([FromQuery]int? contentCount, bool? needShuffle, int? contentType, [FromQuery] List<int>? tagId,
            string? orderColumn, [FromServices] IRequestHandler<ContentInformationRequest, IEnumerable<ContentInformation>> requestHandler)
        {
            var request = new ContentInformationRequest(contentCount ?? int.MaxValue)
            {
                TagIds = tagId?.ToList(),
                ContentType = contentType,
                NeedShuffle = needShuffle,
                OrderColumn = orderColumn
            };
            return await requestHandler.HandleAsync(request, default);

        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] long id, [FromBody] ContentWrite content,
            [FromServices] IRequestHandler<UpdateContentRequest, Content> requestHandler)
        {
            await requestHandler.HandleAsync(new UpdateContentRequest(id, content),default);
            return Ok();

        }
        [HttpPost]
        public async Task<ActionResult> Add([FromBody]List<ContentWrite> content,[FromServices] IRequestHandler<AddContentRequest, List<Content>> requestHandler)
        {
            await requestHandler.HandleAsync(new AddContentRequest(content),default);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id, [FromServices] IRequestHandler<DeleteContentRequest, bool> requestHandler)
        {
            var result =  await requestHandler.HandleAsync(new DeleteContentRequest() { ContentId = id },default);
            if (!result)
                return NotFound();
            return Ok();
        }
    }
}
