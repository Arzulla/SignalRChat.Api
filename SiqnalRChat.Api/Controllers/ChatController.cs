using Microsoft.AspNetCore.Mvc;
using SiqnalRChat.Api.Models;

namespace SiqnalRChat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController:ControllerBase
    {
        private readonly IChatRepository _chatRepository;

        public ChatController(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        [HttpGet]
        [Route("LoadMessage")]
        public  IActionResult GetMessages([FromQuery] string roomCode,[FromQuery] int skip,[FromQuery] int take)
        {
            var result = _chatRepository.Get(roomCode, skip, take);
            return Ok(result);
        }
    }
}
