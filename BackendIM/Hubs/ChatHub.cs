using BackendIM.Models;
using Microsoft.AspNetCore.SignalR;

namespace BackendIM.Hubs
{
    public class ChatHub : Hub
    {
        private readonly static ConnectionMapping<string> _connections =
           new ConnectionMapping<string>();

        public override Task OnConnectedAsync()
        {
            string name = Context!.User!.Identity!.Name!;

            _connections.Add(name, Context.ConnectionId);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            string name = Context.User.Identity.Name;

            _connections.Remove(name, Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }

    }
}
