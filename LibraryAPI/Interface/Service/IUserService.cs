using LibraryAPI.Common;
using LibraryAPI.Dto.User;

namespace LibraryAPI.Interface.Service
{
    public interface IUserService
    {
        Task<OperationResult<UserDto?>> UserRegister(RegisterDto model);
        Task<OperationResult<UserDto?>> UserLogin(LoginDto model);
    }
}
