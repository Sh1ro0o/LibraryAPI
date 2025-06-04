using LibraryAPI.Data;
using LibraryAPI.Interface;
using LibraryAPI.Interface.Repository;
using LibraryAPI.Repository;

namespace LibraryAPI.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _context;
        private IBookRepository _bookRepository;
        private IAuthorRepository _authorRepository;
        private IBookAuthorRepository _bookAuthorRepository;
        private IBookCopyRepository _bookCopyRepository;
        private IGenreRepository _genreRepository;
        private IBookGenreRepository _bookGenreRepository;
        private IBorrowingTransactionRepository _borrowingTransactionRepository;
        private IAssistantChatRepository _assistantChatRepository;

        public UnitOfWork(LibraryDbContext context)
        {
            _context = context;
        }

        public IBookRepository BookRepository => _bookRepository ??= new BookRepository(_context);
        public IAuthorRepository AuthorRepository => _authorRepository ??= new AuthorRepository(_context);
        public IBookAuthorRepository BookAuthorRepository => _bookAuthorRepository ??= new BookAuthorRepository(_context);
        public IBookCopyRepository BookCopyRepository => _bookCopyRepository ??= new BookCopyRepository(_context);
        public IGenreRepository GenreRepository => _genreRepository ??= new GenreRepository(_context);
        public IBookGenreRepository BookGenreRepository => _bookGenreRepository ??= new BookGenreRepository(_context);
        public IBorrowingTransactionRepository BorrowingTransactionRepository => _borrowingTransactionRepository ??= new BorrowingTransactionRepository(_context);
        public IAssistantChatRepository AssistantChatRepository => _assistantChatRepository ??= new AssistantChatRepository(_context);

        public async Task<int> Commit()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
