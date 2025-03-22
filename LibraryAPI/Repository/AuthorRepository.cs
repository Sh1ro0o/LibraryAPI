using LibraryAPI.Data;
using LibraryAPI.Filters;
using LibraryAPI.Interface.Repository;
using LibraryAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LibraryAPI.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryDbContext _context;
        public AuthorRepository(LibraryDbContext context)
        {
            _context = context;
        }

        #region GET Methods
        public IQueryable<Author> GetAll(AuthorFilter filter)
        {
            var query = _context.Author.AsQueryable();

            if (filter.RecordId != null)
            {
                query = query.Where(x => x.RecordId == filter.RecordId);
            }

            if (!string.IsNullOrEmpty(filter.FirstName))
            {
                query = query.Where(x => x.FirstName == filter.FirstName);
            }

            if (!string.IsNullOrEmpty(filter.LastName))
            {
                query = query.Where(x => x.LastName == filter.LastName);
            }

            if (filter.ExcludeRecordId != null)
            {
                query = query.Where(x => x.RecordId != filter.ExcludeRecordId);
            }

            if (filter.PageNumber != null && filter.PageSize != null)
            {
                query = query.Skip(filter.PageSize.Value * (filter.PageNumber.Value - 1));
                query = query.Take(filter.PageSize.Value);
            }

            query = query.OrderByDescending(x => x.RecordId);

            return query;
        }

        public async Task<Author?> GetById(int id)
        {
            return await _context.Author.FirstOrDefaultAsync(x => x.RecordId == id);
        }

        #endregion

        #region CREATE Methods
        public async Task<Author> CreateAuthor(Author author)
        {
            await _context.Author.AddAsync(author);

            return author;
        }
        #endregion

        #region UPDATE Methods
        public void UpdateAuthor(Author author)
        {
            _context.Author.Update(author);
        }
        #endregion

        #region DELETE Methods
        public void DeleteAuthor(Author author)
        {
            _context.Author.Remove(author);
        }
        #endregion
    }
}
