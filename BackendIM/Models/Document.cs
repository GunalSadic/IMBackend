using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendIM.Models;

public partial class Document
{
    public Guid DocumentId { get; set; }

    public Guid MessageId { get; set; }

    public string DocumentType { get; set; } = null!;

    public string FileName { get; set; } = null!;

    public long DocumentSize { get; set; }

    public byte[] Document1 { get; set; } = null!;

    [JsonIgnore]
    public virtual Message Message { get; set; } = null!;
}
