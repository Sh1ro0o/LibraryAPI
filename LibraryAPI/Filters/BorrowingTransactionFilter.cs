namespace LibraryAPI.Filters
{
    public class BorrowingTransactionFilter
    {
        public int? RecordId { get; set; }
        public DateTime? BorrowDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public bool? IsReturned { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? UserId { get; set; }
        public int? BookCopyId { get; set; }

        public bool IncludeBookCopy { get; set; } = true; //Include BookCopies
        public bool IncludeUser { get; set; } = true; //Include Users
        public bool IncludeBook { get; set; } = false; //Include Books

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
