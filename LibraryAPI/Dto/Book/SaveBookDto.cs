using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Dto.Book
{
    public class SaveBookDto
    {
        [Required]
        public required int RecordId { get; set; }
        [Required]
        public required string Title { get; set; }
        public DateOnly? PublishDate { get; set; }
        public string? ISBN { get; set; }
        public string? Description { get; set; }
    }
}
