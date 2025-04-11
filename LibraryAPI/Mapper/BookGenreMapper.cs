using LibraryAPI.Dto.BookGenre;
using LibraryAPI.Model;

namespace LibraryAPI.Mapper
{
    public static class BookGenreMapper
    {
        public static BookGenreDto ToBookGenreDto(this BookGenre bookGenre)
        {
            return new BookGenreDto
            {
                BookId = bookGenre.BookId,
                GenreId = bookGenre.GenreId
            };
        }
    }
}
