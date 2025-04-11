namespace LibraryAPI.Dto.BookGenre
{
    public class SaveBookGenreDto
    {
        public int BookId { get; set; }
        public List<int> GenreIds { get; set; } = new List<int>();
    }
}
