using LibraryAPI.Data;
using LibraryAPI.Interface;
using LibraryAPI.Repository;

namespace LibraryAPI.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _context;
        private IBookRepository _bookRepository;

        public UnitOfWork(LibraryDbContext context)
        {
            _context = context;
        }

        public IBookRepository BookRepository => _bookRepository ??= new BookRepository(_context);

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
