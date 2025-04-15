using LibraryAPI.Common;
using LibraryAPI.Dto.User;

namespace LibraryAPI.Interface.Service
{
    public interface IUserService
    {
        Task<OperationResult<bool>> UserRegister(RegisterDto model);
    }
}
