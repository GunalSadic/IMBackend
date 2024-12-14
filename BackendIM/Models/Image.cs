using System;
using System.Collections.Generic;

namespace BackendIM.Models;

public partial class Image
{
    public Guid ImageId { get; set; }

    public Guid MessageId { get; set; }

    public string ImageType { get; set; } = null!;

    public long ImageSize { get; set; }

    public byte[] Image1 { get; set; } = null!;

    public virtual Message Message { get; set; } = null!;
}
