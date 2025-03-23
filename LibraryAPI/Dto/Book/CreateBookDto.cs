using LibraryAPI.Dto.Author;
using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Dto.Book
{
    public class CreateBookDto
    {
        [Required]
        public required string Title { get; set; }
        public DateOnly? PublishDate { get; set; }
        public string? ISBN { get; set; }
        public string? Description { get; set; }
        public List<int>? AuthorIds { get; set; } = new List<int>();
    }
}
