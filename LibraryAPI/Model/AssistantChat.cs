using LibraryAPI.Common.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Model
{
    public class AssistantChat
    {
        [Key]
        [Column("Id")]
        public int RecordId { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public string SenderType { get; set; } = Common.Constants.SenderType.Undefined;

        public string UserId { get; set; } = string.Empty;
        public AppUser? User { get; set; }
    }
}
