using ContentGuess.Application.Dto;
using ContentGuess.Application.TagHandlers;
using ContentGuess.Domain;
using ContentGuess.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;



namespace ContentGuess.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagRead>>> Get([FromServices] IRequestHandler<GetTagListRequest, List<TagRead>> requestHandler)
        {
            return await requestHandler.HandleAsync(new GetTagListRequest(), default);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<TagRead>> Get(int id,[FromBody] IRequestHandler<GetTagRequest, TagRead?> requestHandler)
        {
            var tag = await requestHandler.HandleAsync(new GetTagRequest() { TagId = id }, default);
            if (tag == null)
                return NotFound();
            return Ok(tag);
        }

        
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TagWrite tag, [FromServices] IRequestHandler<AddTagRequest, Tag?> requestHandler)
        {
           var res = await requestHandler.HandleAsync(new AddTagRequest(tag), default);
            if (res == null)
                return BadRequest($"Tag with name {tag.Name} is exists");
            return Ok();
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TagWrite tag,[FromServices] IRequestHandler<UpdateTagRequest, Tag> requestHandler)
        {
            await requestHandler.HandleAsync(new UpdateTagRequest(id,tag), default);
            return Ok();
        }

        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
