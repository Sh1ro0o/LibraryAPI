using LibraryAPI.Model;
using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Dto.BorrowingTransaction
{
    public class BorrowingTransactionDto
    {
        public int RecordId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public bool IsReturned { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        //User
        [Required(AllowEmptyStrings = false)]
        public string UserId { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;

        //BookCopy
        [Required]
        public int BookCopyId { get; set; }
        public string BookCopySerialNumber { get; set; } = string.Empty;
    }
}
