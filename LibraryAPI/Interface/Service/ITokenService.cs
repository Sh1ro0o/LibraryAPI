using LibraryAPI.Dto.Token;
using LibraryAPI.Model;

namespace LibraryAPI.Interface.Service
{
    public interface ITokenService
    {
        TokenDto CreateToken(AppUser user);
    }
}
