using Microsoft.AspNetCore.SignalR;

namespace SwissChatClient.Helpers
{
    public class ChatHub : Hub
    {
        public async Task SendMessageToGroup(string groupName, string user, string message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendMessageToUser(string userId, string user, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveMessage", user, message);
        }
    }
}
