using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Dto.BorrowingTransaction
{
    public class CreateBorrowingTransactionDto
    {
        [Required]
        public DateTime DueDate { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string UserId { get; set; } = string.Empty;  

        [Required]
        public int BookCopyId { get; set; }
    }
}
