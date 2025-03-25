using LibraryAPI.Data;
using LibraryAPI.Dto;
using LibraryAPI.Filters;
using LibraryAPI.Interface;
using LibraryAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;
        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAll(BookFilter filter)
        {
            return await GetBooksFilteredInternal(filter).ToListAsync();
        }

        public async Task<Book?> GetOne(BookFilter filter)
        {
            return await GetBooksFilteredInternal(filter).FirstOrDefaultAsync();
        }

        public async Task<Book?> GetById(int id)
        {
            return await _context.Book
                .Include(x => x.BookAuthors).ThenInclude(x => x.Author)
                .FirstOrDefaultAsync(x => x.RecordId == id);
        }

        public async Task<Book> Create(Book model)
        {
            await _context.Book.AddAsync(model);

            return model;
        }

        public void Update(Book book)
        {
            _context.Book.Update(book);
        }

        public void Delete(Book book)
        {
            _context.Book.Remove(book);
        }


        //Internal Methods
        private IQueryable<Book> GetBooksFilteredInternal(BookFilter filter)
        {
            var query = _context.Book.AsQueryable();

            // Apply filters based on the filter object
            if (filter.RecordId != null)
            {
                query = query.Where(x => x.RecordId == filter.RecordId);
            }

            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                query = query.Where(x => x.Title == filter.Title);
            }

            if (filter.PublishDate != null)
            {
                query = query.Where(x => x.PublishDate == filter.PublishDate);
            }

            if (!string.IsNullOrWhiteSpace(filter.ISBN))
            {
                query = query.Where(x => x.ISBN == filter.ISBN);
            }

            // Include authors if requested
            if (filter.IncludeAuthors)
            {
                query = query.Include(x => x.BookAuthors).ThenInclude(x => x.Author);
            }

            // Apply pagination if provided
            if (filter.PageNumber != null && filter.PageSize != null)
            {
                query = query.Skip(filter.PageSize.Value * (filter.PageNumber.Value - 1));
                query = query.Take(filter.PageSize.Value);
            }

            // Apply sorting
            query = query.OrderByDescending(x => x.RecordId);

            return query;
        }
    }
}
