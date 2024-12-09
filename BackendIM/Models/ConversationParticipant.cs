using System;
using System.Collections.Generic;

namespace BackendIM.Models;

public partial class ConversationParticipant
{
    public int ParticipantId { get; set; }

    public int UserId { get; set; }

    public int ConversationId { get; set; }

    public virtual Conversation Conversation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
