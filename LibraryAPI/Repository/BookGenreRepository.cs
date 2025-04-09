using LibraryAPI.Data;
using LibraryAPI.Interface.Repository;
using LibraryAPI.Model;

namespace LibraryAPI.Repository
{
    public class BookGenreRepository : IBookGenreRepository
    {
        private readonly LibraryDbContext _context;
        public BookGenreRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<BookGenre>> CreateBookGenreConnections(Book book, List<int> genreIds)
        {
            var bookGenres = genreIds.Select(x => new BookGenre
            {
                Book = book,
                GenreId = x
            }).ToList();

            await _context.BookGenre.AddRangeAsync(bookGenres);

            return bookGenres;
        }
    }
}
