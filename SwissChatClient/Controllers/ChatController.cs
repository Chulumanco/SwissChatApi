using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

using SwissChatClient.Helpers;
using SwissChatClient.Models;
using static SwissChatClient.Models.ChatsViewModel;

namespace SwissChatClient.Controllers
{
    [ApiController]
    [Route("api/chat")]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _chatHubContext;
        private ISessionHelpers _sessionHelpers;

        public ChatController(IHubContext<ChatHub> chatHubContext, ISessionHelpers sessionHelpers)
        {
            _chatHubContext = chatHubContext;
            _sessionHelpers = sessionHelpers;
        }

        [HttpPost("group")]
        public async Task<IActionResult> SendMessageToGroup(string groupName, [FromBody] SendMessageModel message)
        {
            await _chatHubContext.Clients.Group(groupName).SendAsync("ReceiveMessage", message.UserName, message.Message);
            return Ok();
        }

        [HttpPost("user")]
        public async Task<IActionResult> SendMessageToUser(string userId, [FromBody] SendMessageModel message)
        {
            await _chatHubContext.Clients.User(userId).SendAsync("ReceiveMessage", message.UserName, message.Message);
            return Ok();
        }
       
        public List<string> GetSessionList()
        {
            // Retrieve the list from session
            var session = _sessionHelpers.GetList<string>(HttpContext.Session, "UserSession");
            return session;
        }
        private string GetObject(int obj)
        {
            var token = GetSessionList();
            var i = token.ElementAt(obj);
            return i;

        }
    }
}
