namespace LibraryAPI.Dto.BorrowingTransaction
{
    public class SeperateTransactionsCountDto
    {
        public int BorrowedBooksCount { get; set; }
        public int ReturnedBooksCount { get; set; }
        public int OverdueBooksCount { get; set; }
    }
}
