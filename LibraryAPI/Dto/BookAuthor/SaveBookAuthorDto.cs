namespace LibraryAPI.Dto.BookAuthor
{
    public class SaveBookAuthorDto
    {
        public int BookId { get; set; }
        public List<int> AuthorIds { get; set; } = new List<int>();
    }
}
