using Microsoft.AspNetCore.SignalR;

namespace BackendIM.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            Clients.All.SendAsync("Receive Message", user, message);
        }
    }
}
