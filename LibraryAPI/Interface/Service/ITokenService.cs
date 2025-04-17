using LibraryAPI.Model;

namespace LibraryAPI.Interface.Service
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
