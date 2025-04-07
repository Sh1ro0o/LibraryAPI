using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Dto.BookCopy
{
    public class SaveBookCopyDto
    {
        [Required]
        public required int RecordId { get; set; }
        public bool? IsAvailable { get; set; }
        public int? BookId { get; set; }
    }
}
