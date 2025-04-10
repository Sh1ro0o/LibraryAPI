using LibraryAPI.Data;
using LibraryAPI.Interface.Repository;
using LibraryAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LibraryAPI.Repository
{
    public class BookAuthorRepository : IBookAuthorRepository
    {
        private readonly LibraryDbContext _context;

        public BookAuthorRepository(LibraryDbContext context)
        {
            _context = context;
        }

        #region GET Methods

        public async Task<List<BookAuthor>> GetByBookId(int bookId)
        {
            var existingBookAuthors = await _context.BookAuthor
                .Where(x => x.BookId == bookId)
                .ToListAsync();

            return existingBookAuthors;
        }

        #endregion

        #region CREATE Methods

        public async Task AddRange(List<BookAuthor> bookAuthors)
        {
            await _context.BookAuthor.AddRangeAsync(bookAuthors);
        }

        #endregion

        #region DELETE Methods

        public void DeleteRange(List<BookAuthor> bookAuthors)
        {
            _context.BookAuthor.RemoveRange(bookAuthors);
        }

        #endregion



        public async Task<List<BookAuthor>> CreateBookAuthorConnections(Book book, List<int> authorIds)
        {
            var bookAuthors = authorIds.Select(x => new BookAuthor
            {
                Book = book,
                AuthorId = x
            }).ToList();

            await _context.BookAuthor.AddRangeAsync(bookAuthors);

            return bookAuthors;
        }

        public async Task UpdateBookAuthorConnections(int bookId, List<int> authorIds)
        {
            var existingBookAuthors = await _context.BookAuthor
                .Where(x => x.BookId == bookId)
                .ToListAsync();

            var existingBookAuthorIds = existingBookAuthors.Select(x => x.AuthorId).ToList();

            var bookAuthorsToDelete = existingBookAuthors
                .Where(x => !authorIds.Contains(x.AuthorId))
                .ToList();

            var bookAuthorsToAdd = authorIds
                .Where(x => !existingBookAuthorIds.Contains(x))
                .Select(x => new BookAuthor
                {
                    BookId = bookId,
                    AuthorId = x
                }).ToList();

            await _context.BookAuthor.AddRangeAsync(bookAuthorsToAdd);

            _context.BookAuthor.RemoveRange(bookAuthorsToDelete);
        }
    }
}
