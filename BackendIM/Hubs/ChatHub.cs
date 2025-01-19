using System.Collections.ObjectModel;
using BackendIM.Helpers;
using BackendIM.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
            if (!message.IsEdited)
            {
                _dbContext.Messages.Add(message);
             
            }
            else
            {
                var msg = _dbContext.Messages.First(x => x.MessageId == message.MessageId); msg.Text = message.Text; msg.IsEdited = true;
            }
                
            await _dbContext.SaveChangesAsync();
        }
        public async Task SendMediaMessage(Guid messageId, Guid conversationId)
        {
            // Retrieve the conversation and associated participants
            Conversation conversation = _dbContext.Conversations
                .Where(x => x.ConversationId == conversationId)
                .Include(c => c.ConversationParticipants)
                .FirstOrDefault();

            if (conversation == null)
            {
                throw new Exception("Conversation not found.");
            }

            // Retrieve the message, including the documents
            Message message = _dbContext.Messages
                .Where(x => x.MessageId == messageId)
                .Include(m => m.Documents)
                .Include(m => m.Sender)
                .FirstOrDefault();

            if (message == null)
            {
                throw new Exception("Message not found.");
            }
            List<string> connectedUserIds = GetAllConnectedUsersConnectionIds(conversation.ConversationParticipants);

            // Send the message JSON to all connected users in the conversation
            foreach (string connectionId in connectedUserIds)
            {
                Console.WriteLine($"Sending media to connection: {connectionId}");
                await Clients.Client(connectionId).SendAsync("ReceiveMediaMessage", message);
            }
            string ftpServer = "ftp://win6050.site4now.net";
            string username = "aamsteam-001";
            string password = "IMPassword1!";
            string filePath = $"{ftpServer}/{message.Sender.UserName}/{message.Documents.First().FileName}";
            FTPHelper.UploadFile(message.Documents.First().Document1, filePath, username, password);
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

        private List<string> GetAllConnectedUsersConnectionIds(ICollection<ConversationParticipant> participants)
        {
            List<string> connectionIds = new List<string>();
            foreach (var participant in participants)
            {
                var connectionsForUser = _connections.GetConnections(participant.UserId);
                connectionIds.AddRange(connectionsForUser);
            }
            return connectionIds;
        }
    }
}
