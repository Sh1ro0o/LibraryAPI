using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Dto.BookCopy
{
    public class CreateBookCopyDto
    {
        [Required]
        public required int BookId { get; set; }
    }
}
