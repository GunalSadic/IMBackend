using System;
using System.Collections.Generic;

namespace BackendIM.Models;

public partial class VoiceRecording
{
    public int VoiceRecordingId { get; set; }

    public int MessageId { get; set; }

    public string AudioType { get; set; } = null!;

    public long AudioSize { get; set; }

    public byte[] Audio { get; set; } = null!;

    public virtual Message Message { get; set; } = null!;
}
