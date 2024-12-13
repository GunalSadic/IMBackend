
using Microsoft.AspNetCore.Identity;

namespace BackendIM.Models;

public partial class User : IdentityUser
{
    public string UserId { get; set; }

    public string? ProfilePicture { get; set; }

    public virtual ICollection<ConversationParticipant> ConversationParticipants { get; set; } = new List<ConversationParticipant>();

    public virtual ICollection<Message> MessageReceivers { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageSenders { get; set; } = new List<Message>();
}
