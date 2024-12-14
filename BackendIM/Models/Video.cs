using System;
using System.Collections.Generic;

namespace BackendIM.Models;

public partial class Video
{
    public Guid VideoId { get; set; }

    public Guid MessageId { get; set; }

    public string VideoType { get; set; } = null!;

    public long VideoSize { get; set; }

    public byte[] Video1 { get; set; } = null!;

    public virtual Message Message { get; set; } = null!;
}
