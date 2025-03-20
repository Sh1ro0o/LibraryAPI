using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Model
{
    public class Author
    {
        [Key]
        [Column("Id")]
        public int RecordId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public ICollection<BookAuthor>? BookAuthors { get; set; }
    }
}
