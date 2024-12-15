using System;
using System.Collections.Generic;

namespace BackendIM.Models;

public partial class Conversation
{
    public Guid ConversationId { get; set; }

    public string? GroupPicture { get; set; }

    public bool IsGroupChat { get; set; }

    public string? GroupName { get; set; }
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<ConversationParticipant> ConversationParticipants { get; set; } = new List<ConversationParticipant>();

    public virtual Message? LastSentMessage { get; set; }
}
