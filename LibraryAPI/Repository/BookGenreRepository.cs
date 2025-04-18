using LibraryAPI.Data;
using LibraryAPI.Interface.Repository;
using LibraryAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repository
{
    public class BookGenreRepository : IBookGenreRepository
    {
        private readonly LibraryDbContext _context;
        public BookGenreRepository(LibraryDbContext context)
        {
            _context = context;
        }

        #region GET Methods

        public async Task<ICollection<BookGenre>> GetByBookId(int bookId)
        {
            var existingBookGenres = await _context.BookGenre
                .Where(x => x.BookId == bookId)
                .ToListAsync();

            return existingBookGenres;
        }

        #endregion

        #region CREATE Methods

        public async Task AddRange(ICollection<BookGenre> bookGenres)
        {
            await _context.BookGenre.AddRangeAsync(bookGenres);
        }

        #endregion

        #region DELETE Methods

        public void DeleteRange(ICollection<BookGenre> bookGenres)
        {
            _context.BookGenre.RemoveRange(bookGenres);
        }

        #endregion
    }
}
