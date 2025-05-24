using LibraryAPI.Dto.Token;
using LibraryAPI.Model;

namespace LibraryAPI.Interface.Service
{
    public interface ITokenService
    {
        Task<TokenDto> CreateToken(AppUser user);
    }
}
