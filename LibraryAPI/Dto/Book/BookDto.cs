namespace LibraryAPI.Dto.Book
{
    public class BookDto
    {
        public int RecordId { get; set; }
        public required string Title { get; set; }
        public DateOnly? PublishDate { get; set; }
        public string? ISBN { get; set; }
        public string? Description { get; set; }
        public int? AuthorId { get; set; }
        public string? AuthorFirstName { get; set; }
        public string? AuthorLastName { get; set; }
    }
}
