namespace LibraryAPI.Dto.Book
{
    public class SaveBookDto
    {
        public required string Title { get; set; }
        public DateOnly? PublishDate { get; set; }
        public string? ISBN { get; set; }
        public string? Description { get; set; }
    }
}
