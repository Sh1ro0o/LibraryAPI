using LibraryAPI.Data;
using LibraryAPI.Filters;
using LibraryAPI.Interface.Repository;
using LibraryAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repository
{
    public class BookCopyRepository : IBookCopyRepository
    {
        private readonly LibraryDbContext _context;

        public BookCopyRepository(LibraryDbContext context)
        {
            _context = context;
        }

        #region GET methods

        public async Task<ICollection<BookCopy>> GetAll(BookCopyFilter filter)
        {
            return await GetFilteredBookCopiesInternal(filter).ToListAsync();
        }
        public async Task<BookCopy?> GetOne(BookCopyFilter filter)
        {
            return await GetFilteredBookCopiesInternal(filter).FirstOrDefaultAsync();
        }
        public async Task<BookCopy?> GetById(int id)
        {
            return await _context.BookCopy.FirstOrDefaultAsync(x => x.RecordId == id);
        }

        #endregion

        #region CREATE methods

        public async Task<BookCopy> Create(BookCopy model)
        {
            await _context.BookCopy.AddAsync(model);

            return model;
        }

        #endregion

        #region UPDATE methods

        public void Update(BookCopy bookCopy)
        {
            _context.BookCopy.Update(bookCopy);
        }

        #endregion

        #region DELETE methods

        public void Delete(BookCopy bookCopy)
        {
            _context.BookCopy.Remove(bookCopy);
        }

        #endregion

        #region PRIVATE METHODS

        private IQueryable<BookCopy> GetFilteredBookCopiesInternal(BookCopyFilter filter)
        {
            var query = _context.BookCopy.AsQueryable();

            if (filter.RecordId != null)
            {
                query = query.Where(x => x.RecordId == filter.RecordId);
            }
            if (!string.IsNullOrWhiteSpace(filter.SerialNumber))
            {
                query = query.Where(x => x.SerialNumber.ToLower() == filter.SerialNumber.ToLower());
            }
            if (filter.IsAvailable != null)
            {
                query = query.Where(x => x.IsAvailable == filter.IsAvailable);
            }
            if (filter.CreateDate != null)
            {
                query = query.Where(x => x.CreateDate >= filter.CreateDate);
            }
            if (filter.ModifiedDate != null)
            {
                query = query.Where(x => x.ModifiedDate >= filter.ModifiedDate);
            }
            if (filter.BookId != null)
            {
                query = query.Where(x => x.BookId == filter.BookId);
            }
            if (filter.IncludeBook)
            {
                query = query.Include(x => x.Book);
            }

            query = query.Include(x => x.Book);

            if (filter.PageNumber != null && filter.PageSize != null)
            {
                query = query.Skip((filter.PageNumber.Value - 1) * filter.PageSize.Value);
                query = query.Take(filter.PageSize.Value);
            }

            // Apply sorting
            query = query.OrderByDescending(x => x.RecordId);

            return query;
        }

        #endregion
    }
}
