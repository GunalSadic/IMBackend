using System;
using System.Collections.Generic;

namespace BackendIM.Models;

public partial class Message
{
    public int MessageId { get; set; }

    public DateTime SentTime { get; set; }

    public bool IsEdited { get; set; }

    public string Text { get; set; } = null!;

    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

    public int ConversationId { get; set; }

    public string? EmbeddedResourceType { get; set; }

    public bool IsScheduled { get; set; }

    public virtual ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual User Receiver { get; set; } = null!;

    public virtual ScheduledMessage? ScheduledMessage { get; set; }

    public virtual User Sender { get; set; } = null!;

    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();

    public virtual ICollection<VoiceRecording> VoiceRecordings { get; set; } = new List<VoiceRecording>();
}
