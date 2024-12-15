using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendIM.Models;

public partial class ConversationParticipant
{
    public Guid ParticipantId { get; set; }

    public string UserId { get; set; }

    public Guid ConversationId { get; set; }

    [JsonIgnore]
    public virtual Conversation Conversation { get; set; } = null!;

    public virtual User User { get; set; } = null!;

}
