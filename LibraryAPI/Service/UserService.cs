using LibraryAPI.Common;
using LibraryAPI.Common.Constants;
using LibraryAPI.Dto.Book;
using LibraryAPI.Dto.User;
using LibraryAPI.Interface.Service;
using LibraryAPI.Model;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace LibraryAPI.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<OperationResult<bool>> UserRegister(RegisterDto model)
        {
            //Check if user with Email already exists
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return OperationResult<bool>.Conflict(false, "A user with this email already exists.");
            }

            var newUser = new AppUser
            {
                Email = model.Email,
                UserName = model.Email
            };

            var createdUser = await _userManager.CreateAsync(newUser, model.Password);

            if (createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(newUser, Roles.User);
                if (roleResult.Succeeded)
                {
                    //Return success
                    return OperationResult<bool>.Success(true);
                }
                else
                {
                    //Return internal server error
                    return OperationResult<bool>.InternalServerError(false, GetIdentityErrors(roleResult.Errors));
                }
            }
            else
            {
                //return internal server error
                return OperationResult<bool>.InternalServerError(false, GetIdentityErrors(createdUser.Errors));
            }
        }

        #region PRIVATE Methods

        private string GetIdentityErrors(IEnumerable<IdentityError> errors)
        {
            return string.Join(", ", errors.Select(e => e.Description));
        }

        #endregion
    }
}
