using LibraryAPI.Common.Response;
using LibraryAPI.Filters;
using LibraryAPI.Model;

namespace LibraryAPI.Interface.Repository
{
    public interface IRefreshTokenRepository
    {
        Task<ICollection<RefreshToken>> GetAll(RefreshTokenFilter filter);
        Task<RefreshToken?> GetOne(RefreshTokenFilter filter);
        Task<RefreshToken?> GetById(int id);
        Task<RefreshToken> Create(RefreshToken model);
        void Update(RefreshToken refreshToken);
        void Delete(RefreshToken refreshToken);
    }
}
