using BackendIM.Models;

namespace BackendIM.Helpers
{
    public class FileStorageHelper
    {
        public static void SaveFileToDbContext(object file, ApplicationDbContext _dbContext, Guid attachedMessageId)
        {
            switch (file)
            {
                case Image image:
                    image.MessageId = attachedMessageId;
                    _dbContext.Images.Add(image);
                    break;
                case Video video:
                    video.MessageId = attachedMessageId;
                    _dbContext.Videos.Add(video);
                    break;
                case VoiceRecording voiceRecording:
                    voiceRecording.MessageId = attachedMessageId;
                    _dbContext.VoiceRecordings.Add(voiceRecording);
                    break;
                case Document document:
                    document.MessageId = attachedMessageId;
                    _dbContext.Documents.Add(document);
                    break;
                default:
                    throw new ArgumentException($"Unsupported file type: {file.GetType().Name}");
            }
        }
    }
}
