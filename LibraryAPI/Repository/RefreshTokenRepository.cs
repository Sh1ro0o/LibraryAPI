using LibraryAPI.Data;
using LibraryAPI.Filters;
using LibraryAPI.Interface.Repository;
using LibraryAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace LibraryAPI.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly LibraryDbContext _context;
        public RefreshTokenRepository(LibraryDbContext context)
        {
            _context = context;
        }

        async Task<ICollection<RefreshToken>> IRefreshTokenRepository.GetAll(RefreshTokenFilter filter)
        {
            return await GetRefreshTokensFilteredInternal(filter).ToListAsync();
        }
        async Task<RefreshToken?> IRefreshTokenRepository.GetOne(RefreshTokenFilter filter)
        {
            return await GetRefreshTokensFilteredInternal(filter).FirstOrDefaultAsync();
        }

        async Task<RefreshToken?> IRefreshTokenRepository.GetById(int id)
        {
            return await _context.RefreshToken.FirstOrDefaultAsync(x => x.RecordId == id);
        }

        async Task<RefreshToken> IRefreshTokenRepository.Create(RefreshToken model)
        {
            await _context.AddAsync(model);

            return model;
        }

        void IRefreshTokenRepository.Update(RefreshToken refreshToken)
        {
            _context.Update(refreshToken);
        }

        void IRefreshTokenRepository.Delete(RefreshToken refreshToken)
        {
            _context.Remove(refreshToken);
        }


        //Internal Methods
        private IQueryable<RefreshToken> GetRefreshTokensFilteredInternal(RefreshTokenFilter filter)
        {
            var query = _context.RefreshToken.AsQueryable();

            if (filter.RecordId != null)
            {
                query = query.Where(x => x.RecordId == filter.RecordId);
            }

            if (!string.IsNullOrWhiteSpace(filter.UserId))
            {
                query = query.Where(x => x.UserId == filter.UserId);
            }

            if (filter.ExpiresAt != null)
            {
                query = query.Where(x => x.ExpiresAt >= filter.ExpiresAt);
            }

            if (filter.CreatedAt != null)
            {
                query = query.Where(x => x.CreatedAt >= filter.CreatedAt);
            }

            if (filter.RevokedAt != null)
            {
                query = query.Where(x => x.RevokedAt >= filter.RevokedAt);
            }

            if (filter.IsActive != null)
            {
                query = query.Where(x => x.RevokedAt == null && x.ExpiresAt > DateTime.UtcNow);
            }

            if (filter.IncludeUser)
            {
                query = query.Include(x => x.User);
            }

            query = query.OrderByDescending(x => x.RecordId);

            return query;
        }
    }
}
