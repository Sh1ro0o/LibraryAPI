using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Model
{
    public class Book
    {
        [Key]
        [Column("Id")]
        public int RecordId { get; set; }
        public required string Title { get; set; }
        public DateOnly? PublishDate { get; set; }
        public string? ISBN { get; set; }
        public string? Description { get; set; }

        public ICollection<BookAuthor>? BookAuthors { get; set; }
    }
}
