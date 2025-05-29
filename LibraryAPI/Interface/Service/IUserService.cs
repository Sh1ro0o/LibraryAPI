using LibraryAPI.Common;
using LibraryAPI.Dto.User;
using LibraryAPI.Filters;

namespace LibraryAPI.Interface.Service
{
    public interface IUserService
    {
        Task<OperationResult<UserAuthenticationDto?>> UserRegister(RegisterDto model);
        Task<OperationResult<UserAuthenticationDto?>> UserLogin(LoginDto model);
        Task<OperationResult<IEnumerable<UserDto?>>> GetAll(UserFilter userFilter);
        Task<OperationResult<UserDto?>> GetByEmail(string email);
    }
}
