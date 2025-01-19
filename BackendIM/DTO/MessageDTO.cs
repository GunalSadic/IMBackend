namespace BackendIM.DTO
{
    public class MessageDto
    {
        public DateTime SentTime { get; set; }
        public bool IsEdited { get; set; }
        public string Text { get; set; }
        public string SenderId { get; set; }
        public Guid ConversationId { get; set; }
        public string? EmbeddedResourceType { get; set; }
        public bool IsScheduled { get; set; }
        public List<DocumentDto> Documents { get; set; } = new();
    }

    public class DocumentDto
    {
        public Guid DocumentId { get; set; }
        public string FileName { get; set; }
        public string DocumentType { get; set; }
        public string Document1 { get; set; } // Base64 encoded file data
    }
}
