using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

using SwissChatClient.Helpers;
using SwissChatClient.Models;
using System.Reflection;
using System.Text.Json.Nodes;
using static SwissChatClient.Models.ChatsViewModel;
using static SwissChatClient.Models.UserViewModel;

namespace SwissChatClient.Controllers
{
    [ApiController]
    [Route("/api/chat")]
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
        [HttpPost("sendToUser")]
        public async Task<IActionResult> SendMessage(string username, string message)
        {
            
            // Implement logic to send the message
            await _chatHubContext.Clients.All.SendAsync("ReceiveMessage", username, message);
            return Ok();
        }




    }
}
