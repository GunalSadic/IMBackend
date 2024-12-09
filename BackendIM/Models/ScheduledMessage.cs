using System;
using System.Collections.Generic;

namespace BackendIM.Models;

public partial class ScheduledMessage
{
    public int ScheduledMessageId { get; set; }

    public int MessageId { get; set; }

    public DateTime ScheduledDateTime { get; set; }

    public virtual Message Message { get; set; } = null!;
}
