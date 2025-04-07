namespace LibraryAPI.Dto.BookCopy
{
    public class BookCopyDto
    {
        public required int RecordId { get; set; }
        public required string SerialNumber { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int BookId { get; set; }
    }
}
