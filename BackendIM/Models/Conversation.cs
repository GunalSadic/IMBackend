using System;
using System.Collections.Generic;

namespace BackendIM.Models;

public partial class Conversation
{
    public int ConversationId { get; set; }

    public int? LastSentMessageId { get; set; }

    public string? GroupPicture { get; set; }

    public bool IsGroupChat { get; set; }

    public virtual ICollection<ConversationParticipant> ConversationParticipants { get; set; } = new List<ConversationParticipant>();

    public virtual Message? LastSentMessage { get; set; }
}
