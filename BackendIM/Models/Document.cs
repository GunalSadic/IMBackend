using System;
using System.Collections.Generic;

namespace BackendIM.Models;

public partial class Document
{
    public Guid DocumentId { get; set; }

    public Guid MessageId { get; set; }

    public string DocumentType { get; set; } = null!;

    public long DocumentSize { get; set; }

    public byte[] Document1 { get; set; } = null!;

    public virtual Message Message { get; set; } = null!;
}
