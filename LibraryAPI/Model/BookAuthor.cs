using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Model
{
    public class BookAuthor
    {
        [Key]
        [Column("Id")]
        public int RecordId { get; set; }

        //Book
        public int BookId { get; set; }
        public Book? Book { get; set; }

        //Author
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}
