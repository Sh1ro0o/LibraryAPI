using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Dto.AssistantChat
{
    public class AssistantMessageRequest
    {
        [Required]
        public required string Message { get; set; }
    }
}
