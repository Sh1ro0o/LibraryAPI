using LibraryAPI.Data;
using LibraryAPI.Interface.Repository;
using LibraryAPI.Model;

namespace LibraryAPI.Repository
{
    public class BookAuthorRepository : IBookAuthorRepository
    {
        private readonly LibraryDbContext _context;

        public BookAuthorRepository(LibraryDbContext context)
        {
            _context = context;
        }

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
    }
}
