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

        public IQueryable<Book> GetAll(BookFilter filter)
        {
            var query = _context.Book.AsQueryable();

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

            //Check if include authors
            if (filter.IncludeAuthors)
            {
                query = query
                    .Include(x => x.BookAuthors).ThenInclude(x => x.Author);
            }

            if (filter.PageNumber != null && filter.PageSize != null)
            {
                query = query.Skip(filter.PageSize.Value * (filter.PageNumber.Value - 1));
                query = query.Take(filter.PageSize.Value);
            }

            query = query.OrderByDescending(x => x.RecordId);

            return query;
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
    }
}
