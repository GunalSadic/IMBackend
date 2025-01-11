using BackendIM.Models;

namespace BackendIM.Helpers
{
    public static class FileFactory
    {
        public static object CreateFile(string fileType, byte[] fileData)
        {
            return fileType.ToLower() switch
            {
                "image" => new Image
                {
                    ImageType = fileType,
                    Image1 = fileData,
                },
                "document" => new Document
                {
                    DocumentType = fileType,
                    Document1 = fileData
                },
                "video" => new Video
                {
                    VideoType = fileType,
                    Video1 = fileData
                },
                "voicerecording" => new VoiceRecording
                {
                    AudioType = fileType,
                    Audio = fileData,
                },
                _ => throw new ArgumentException($"Unsupported file type: {fileType}")
            };
        }
    }
}
