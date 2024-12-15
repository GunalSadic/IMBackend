using System.Collections.ObjectModel;
using BackendIM.Models;
using BackendIM.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BackendIM.Hubs
{
    public class ChatHub : Hub
    {
        private readonly static ConnectionMapping<string> _connections =
           new ConnectionMapping<string>();
        private readonly ApplicationDbContext _dbContext;
        public ChatHub(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public string joinHub(string userId)
        {
            _connections.Add(userId, Context.ConnectionId);
            return "Ok";
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _connections.Remove(Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }

        public async Task CreateConversation(Conversation conversation, List<string> participants)
        {
            _dbContext.Conversations.Add(conversation);
            foreach (var participant in participants) {
                new ConversationParticipant { ConversationId = conversation.ConversationId, UserId = participant };
            }
            _dbContext.SaveChanges();
        }
        public async Task SendMessage(Message message)
        {
            Conversation conversation = _dbContext.Conversations.Where(x => x.ConversationId == message.ConversationId).Include(c=>c.ConversationParticipants).First();
            List<string> ConnectedUserIds = GetConnectedUserConnectionIds(conversation.ConversationParticipants, message.SenderId);
            foreach (string connectionId in ConnectedUserIds) {
                Console.WriteLine($"Sending message to connection: {connectionId}");
                await Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
            }
            _dbContext.Messages.Add(message);
            _dbContext.SaveChanges();
        }

        private List<string> GetConnectedUserConnectionIds(ICollection<ConversationParticipant> participants, string senderId)
        {
            List<string> connectionIds = new List<string>();
            foreach (var participant in participants)
            {
                if (participant.UserId == senderId)
                    continue;
                var connectionsForUser = _connections.GetConnections(participant.UserId);
                connectionIds.AddRange(connectionsForUser);
            }
            return connectionIds;
        }
    }
}
