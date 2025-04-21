using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Model
{
    public class BorrowingTransaction
    {
        [Key]
        [Column("Id")]
        public int RecordId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public bool IsReturned { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        //User
        public string UserId { get; set; } = string.Empty;
        public AppUser? User { get; set; }

        //BookCopy
        public int BookCopyId { get; set; }
        public BookCopy? BookCopy { get; set; }
    }
}
