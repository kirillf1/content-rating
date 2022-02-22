using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rating.Application.Rooms;
using Rating.Domain;
using Rating.Domain.Interfaces;

namespace Rating.Hub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoomsController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<string>> CreateRoom([FromServices] IRequestHandler<CreateRoomRequest, Guid> requestHandler,
            [FromBody] CreateRoomRequest request)
        {
            var id = await requestHandler.HandleAsync(request, default);
            Console.WriteLine(id);
            return id.ToString();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateRoom([FromRoute] string id, [FromBody] UpdateRoomRequest request, 
            [FromServices] IRequestHandler<UpdateRoomRequest, bool> requestHandler)
        {

            if (await requestHandler.HandleAsync(request, default))
                return Ok(true);
            else
                return BadRequest("Can't update room");
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(string id,[FromServices] IRequestHandler<GetRoomQuery, Room> requestHandler)
        {
            Guid guid;
            if (!Guid.TryParse(id, out guid))
                return BadRequest("Can't find room");
            return await requestHandler.HandleAsync(new GetRoomQuery(guid), default);
        }
    }
}
