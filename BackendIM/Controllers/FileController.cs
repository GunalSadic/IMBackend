using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BackendIM.Models;
using Microsoft.EntityFrameworkCore;
using BackendIM.DTO;

[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public FileController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromBody] MessageDto messageDto)
    {
        if (messageDto == null)
            return BadRequest("Invalid message data.");

      
            // If the message doesn't exist, create a new one
           Message message = new Message
            {
                MessageId = Guid.NewGuid(),
                SentTime = messageDto.SentTime,
                IsEdited = messageDto.IsEdited,
                Text = messageDto.Text,
                SenderId = messageDto.SenderId,
                ConversationId = messageDto.ConversationId,
                EmbeddedResourceType = messageDto.EmbeddedResourceType,
                IsScheduled = messageDto.IsScheduled,
            };

            _dbContext.Messages.Add(message);
     

        // Process and save documents
        if (messageDto.Documents != null && messageDto.Documents.Any())
        {
            foreach (var docDto in messageDto.Documents)
            {
                var document = new Document
                {
                    DocumentId = docDto.DocumentId != Guid.Empty ? docDto.DocumentId : Guid.NewGuid(),
                    MessageId = message.MessageId,
                    DocumentType = docDto.DocumentType,
                    DocumentSize = docDto.Document1.Length,
                    Document1 = Convert.FromBase64String(docDto.Document1), // Decode Base64 string to byte array
                    FileName = docDto.FileName,
                    Message = message
                };

                _dbContext.Documents.Add(document);
            }
        }

        await _dbContext.SaveChangesAsync();
        return Ok(new
        {
            Message = "Message and file(s) uploaded successfully.",
            MessageId = message.MessageId // Return the message ID
        });
    }


    [HttpGet("download/{id}")]
    public async Task<IActionResult> Download(Guid id)
    {
        var document = await _dbContext.Documents.Include(d => d.Message).FirstOrDefaultAsync(d => d.DocumentId == id);

        if (document == null)
            return NotFound("Document not found.");

        return File(document.Document1, document.DocumentType, $"{document.FileName}");
    }
}
