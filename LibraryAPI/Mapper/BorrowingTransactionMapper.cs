using LibraryAPI.Dto.BorrowingTransaction;
using LibraryAPI.Model;

namespace LibraryAPI.Mapper
{
    public static class BorrowingTransactionMapper
    {
        public static BorrowingTransactionDto ToBorrowingTransactionDto(this BorrowingTransaction borrowingTransaction)
        {
            return new BorrowingTransactionDto
            {
                RecordId = borrowingTransaction.RecordId,
                BorrowDate = borrowingTransaction.BorrowDate,
                DueDate = borrowingTransaction.DueDate,
                ReturnedDate = borrowingTransaction.ReturnedDate,
                IsReturned = borrowingTransaction.IsReturned,
                CreatedDate = borrowingTransaction.CreatedDate,
                ModifiedDate = borrowingTransaction.ModifiedDate,
                UserId = borrowingTransaction.UserId,
                UserEmail = borrowingTransaction.User?.Email ?? string.Empty,
                BookCopyId = borrowingTransaction.BookCopyId,
                BookCopySerialNumber = borrowingTransaction.BookCopy?.SerialNumber ?? string.Empty
            };
        }


    }
}
