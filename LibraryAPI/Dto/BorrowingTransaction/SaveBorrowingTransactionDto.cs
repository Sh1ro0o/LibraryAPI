using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Dto.BorrowingTransaction
{
    public class SaveBorrowingTransactionDto
    {
        [Required]
        public int RecordId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int BookCopyId { get; set; }
    }
}
