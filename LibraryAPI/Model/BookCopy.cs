using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Model
{
    public class BookCopy
    {
        [Key]
        [Column("Id")]
        public int RecordId { get; set; }
        public required string SerialNumber { get; set; } //Scan books when borrowing/returning
        public bool IsAvailable { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }
    }
}
