using LibraryAPI.Common;
using LibraryAPI.Dto.User;

namespace LibraryAPI.Interface.Service
{
    public interface IAuthService
    {
        Task<OperationResult<UserAuthenticationDto?>> RefreshSessionToken(string refreshToken);
    }
}
