using LibraryAPI.Data;
using LibraryAPI.Filters;
using LibraryAPI.Interface.Repository;
using LibraryAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repository
{
    public class BorrowingTransactionRepository : IBorrowingTransactionRepository
    {
        private readonly LibraryDbContext _context;
        public BorrowingTransactionRepository(LibraryDbContext context) {
            _context = context;
        }

        #region GET methods

        public async Task<ICollection<BorrowingTransaction>> GetAll(BorrowingTransactionFilter filter)
        {
            return await GetFilteredBorrowingTransactionsInternal(filter).ToListAsync();
        }
        public async Task<BorrowingTransaction?> GetOne(BorrowingTransactionFilter filter)
        {
            return await GetFilteredBorrowingTransactionsInternal(filter).FirstOrDefaultAsync();
        }

        public async Task<BorrowingTransaction?> GetById(int id)
        {
            return await _context.BorrowingTransaction.FirstOrDefaultAsync(x => x.RecordId == id);
        }

        #endregion

        #region CREATE methods

        public async Task<BorrowingTransaction> Create(BorrowingTransaction model)
        {
            await _context.AddAsync(model);

            return model;
        }

        #endregion

        #region UPDATE methods

        public void Update(BorrowingTransaction model)
        {
            _context.Update(model);
        }

        #endregion


        #region DELETE methods

        public void Delete(BorrowingTransaction model)
        {
            _context.Remove(model);
        }

        #endregion

        #region PRIVATE METHODS

        private IQueryable<BorrowingTransaction> GetFilteredBorrowingTransactionsInternal(BorrowingTransactionFilter filter)
        {
            var query = _context.BorrowingTransaction.AsQueryable();

            //Wheres
            if (filter.RecordId != null)
            {
                query = query.Where(x => x.RecordId == filter.RecordId);
            }

            if (filter.BorrowDate != null)
            {
                query = query.Where(x => x.BorrowDate <= filter.BorrowDate);
            }

            if (filter.DueDate != null)
            {
                query = query.Where(x => x.DueDate <= filter.DueDate);
            }

            if (filter.ReturnedDate != null)
            {
                query = query.Where(x => x.ReturnedDate <= filter.ReturnedDate);
            }

            if (filter.IsReturned != null)
            {
                query = query.Where(x => x.IsReturned == filter.IsReturned);
            }

            if (filter.CreatedDate != null)
            {
                query = query.Where(x => x.CreatedDate <= filter.CreatedDate);
            }

            if (filter.ModifiedDate != null)
            {
                query = query.Where(x => x.ModifiedDate <= filter.ModifiedDate);
            }

            if (!string.IsNullOrWhiteSpace(filter.UserId))
            {
                query = query.Where(x => x.UserId == filter.UserId);
            }

            if (filter.BookCopyId != null)
            {
                query = query.Where(x => x.BookCopyId == filter.BookCopyId);
            }

            //Includes
            if (filter.IncludeBookCopy)
            {
                if (filter.IncludeBook)
                {
                    query = query.Include(x => x.BookCopy).ThenInclude(y => y.Book);
                }
                else
                {
                    query = query.Include(x => x.BookCopy);
                }
            }

            if (filter.IncludeUser)
            {
                query = query.Include(x => x.User);
            }

            //Pagination
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
