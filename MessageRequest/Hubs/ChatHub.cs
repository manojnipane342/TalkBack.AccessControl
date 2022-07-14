using Microsoft.AspNetCore.SignalR;

namespace Hubs.ChatHub
{
    public class ChatHub : Hub                                 
    {
        public Task SendMessage1(string user, string message)         
        {
            return Clients.All.SendAsync("ReceiveOne", user, message);
        }

        //public async Task BroadcastToUser(string data, string userId)
        //=> await Clients.User(userId).SendAsync("broadcasttouser", data);
    }
}
