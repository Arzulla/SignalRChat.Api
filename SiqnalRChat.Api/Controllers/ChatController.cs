using Microsoft.AspNetCore.Mvc;
using SiqnalRChat.Api.Models;
using System.Threading.Tasks;

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
        public async  Task<IActionResult> GetMessages([FromQuery] string roomCode,[FromQuery] int skip,[FromQuery] int take)
        {
            var result =await _chatRepository.GetAsync(roomCode, skip, take);
            return Ok(result);
        }
    }
}
