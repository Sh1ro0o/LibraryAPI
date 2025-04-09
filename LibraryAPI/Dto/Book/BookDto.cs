using LibraryAPI.Dto.Author;
using LibraryAPI.Dto.Genre;

namespace LibraryAPI.Dto.Book
{
    public class BookDto
    {
        public int RecordId { get; set; }
        public required string Title { get; set; }
        public DateOnly? PublishDate { get; set; }
        public string? ISBN { get; set; }
        public string? Description { get; set; }
        public List<AuthorDto>? Authors { get; set; } = new List<AuthorDto>();
        public List<GenreDto>? Genres { get; set; } = new List<GenreDto>();
    }
}
