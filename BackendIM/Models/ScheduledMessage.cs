using System;
using System.Collections.Generic;

namespace BackendIM.Models;

public partial class ScheduledMessage
{
    public Guid ScheduledMessageId { get; set; }

    public Guid MessageId { get; set; }

    public DateTime ScheduledDateTime { get; set; }

    public virtual Message Message { get; set; } = null!;
}
