using BackendIM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendIM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ConversationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("scheduledMessages")]
        public async Task<IActionResult> ScheduleMessage([FromBody] ScheduledMessage message)
        {
            _context.Messages.Add(message.Message);
            _context.ScheduledMessages.Add(message);
            await _context.SaveChangesAsync();

            return Ok(message.Message.Text);
        }

        [HttpGet("user/{userId}/getConversationsForUser")]
        public async Task<IActionResult> GetConversationsForUser(string userId)
        {
            
            var conversations = await _context.Conversations
                .Where(c => c.ConversationParticipants.Any(p => p.UserId == userId))
                .Select(c => new
                {
                    c.ConversationId,
                    Name = c.IsGroupChat ? c.GroupName : c.ConversationParticipants.FirstOrDefault(p => p.UserId != userId).User.UserName,
                    Picture = c.IsGroupChat ? c.GroupPicture : c.ConversationParticipants.FirstOrDefault(p => p.UserId != userId).User.ProfilePicture,
                    LastMessage = c.Messages.OrderByDescending(m => m.SentTime).FirstOrDefault(),
                    c.IsGroupChat
                })
                .ToListAsync();

            return Ok(conversations);
        }

        [HttpGet("{conversationId}/{userId}/getConversationById")]
        public async Task<IActionResult> GetConversationById(Guid conversationId, string userId)
        {

            var conversation = await _context.Conversations
                .Where(c => c.ConversationId == conversationId).Include(x=>x.Messages)
                .Select(c => new
                {
                    c.ConversationId,
                    Name = c.IsGroupChat ? c.GroupName : c.ConversationParticipants.FirstOrDefault(p => p.UserId != userId).User.UserName,
                    Picture = c.IsGroupChat ? c.GroupPicture : c.ConversationParticipants.FirstOrDefault(p => p.UserId != userId).User.ProfilePicture,
                    Messages = c.Messages.Where(x=> x.IsScheduled == false || x.ScheduledMessage == null || x.ScheduledMessage.ScheduledDateTime <= DateTime.Now)
                        .OrderByDescending(m => m.SentTime)
                        .Take(50)
                })
                .FirstOrDefaultAsync();
            if (conversation == null)
            {
                return NotFound();
            }
            return Ok(conversation);
        }

        [HttpPost("createConversation")]
        public async Task<IActionResult> CreateConversation([FromBody] CreateConversationDto dto)
        {
            if (dto.UserId1 == dto.UserId2)
            {
                return BadRequest("A user cannot create a conversation with themselves.");
            }

            var existingConversation = await _context.Conversations
                .FirstOrDefaultAsync(c => !c.IsGroupChat &&
                                          c.ConversationParticipants.Any(p => p.UserId == dto.UserId1) &&
                                          c.ConversationParticipants.Any(p => p.UserId == dto.UserId2));

            if (existingConversation != null)
            {
                return BadRequest("A conversation between these users already exists.");
            }

            var conversation = new Conversation
            {
                ConversationId = Guid.NewGuid(),
                IsGroupChat = false,
                GroupName = null,
                GroupPicture = null,
                ConversationParticipants = new List<ConversationParticipant>
            {
                new ConversationParticipant { UserId = dto.UserId1 },
                new ConversationParticipant { UserId = dto.UserId2 }
            }
            };

            _context.Conversations.Add(conversation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetConversationById), new { conversationId = conversation.ConversationId }, conversation);
        }

        [HttpGet("{conversationId}/GetMessages")]
        public async Task<IActionResult> GetMessages(Guid conversationId, int startIndex, int count)
        {
            var messages = await _context.Messages
                .Where(m => m.ConversationId == conversationId)
                .OrderByDescending(m => m.SentTime)
                .Skip(startIndex)
                .Take(count)
                .ToListAsync();

            return Ok(messages);
        }

        [HttpGet("users/search")]
        public async Task<IActionResult> SearchUsersByUsername([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("Query parameter is required.");
            }

            var matchingUsers = await _context.Users
                .Where(u => EF.Functions.Like(u.UserName.ToLower(), $"%{query.ToLower()}%"))
                .Select(u => new
                {
                    u.Id,
                    u.UserName,
                    u.ProfilePicture
                })
                .ToListAsync();

            return Ok(matchingUsers);
        }

        [HttpGet("user/profile")]
        public IActionResult GetUserProfile(string userName)
        {
            // Example: Fetch user details from the database
            var user = _context.Users
                .Where(u => u.UserName == userName)
                .Select(u => new
                {
                    u.Id,
                    u.UserName,
                    u.ProfilePicture
                })
                .FirstOrDefault();

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }
    }

    public class CreateConversationDto
    {
        public string UserId1 { get; set; }
        public string UserId2 { get; set; }
    }

 
  

}

