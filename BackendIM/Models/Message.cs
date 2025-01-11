using System;
using System.Collections.Generic;

namespace BackendIM.Models;

public partial class Message
{
    public Guid MessageId { get; set; }

    public DateTime SentTime { get; set; }

    public bool IsEdited { get; set; }

    public string Text { get; set; } = null!;

    public string SenderId { get; set; }

    public Guid ConversationId { get; set; }

    public string? EmbeddedResourceType { get; set; }

    public bool IsScheduled { get; set; }

    public Conversation? Conversation { get; set; } = null!;

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ScheduledMessage? ScheduledMessage { get; set; }

    public virtual User? Sender { get; set; } = null!;

    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();

    public virtual ICollection<VoiceRecording> VoiceRecordings { get; set; } = new List<VoiceRecording>();
}
