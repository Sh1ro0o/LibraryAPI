using LibraryAPI.Common;
using LibraryAPI.Model;

namespace LibraryAPI.Interface.Service
{
    public interface IRefreshTokenService
    {
        Task<string?> GetUserIdByRefreshTokenAsync(string token);
        Task<RefreshToken?> ValidateRefreshTokenAsync(string token);
        Task<OperationResult<string?>> CreateAndStoreRefreshTokenAsync(string userId);
        Task<bool> RevokeRefreshTokenAsync(string token);
    }
}
