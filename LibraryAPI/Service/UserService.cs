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
        private readonly ITokenService _tokenService;
        public UserService(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<OperationResult<UserDto?>> UserRegister(RegisterDto model)
        {
            //Check if user with Email already exists
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return OperationResult<UserDto?>.Conflict(message: "A user with this email already exists.");
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
                    var newUserDto = new UserDto
                    {
                        Email = model.Email,
                        Token = _tokenService.CreateToken(newUser)
                    };

                    //Return success
                    return OperationResult<UserDto?>.Success(newUserDto);
                }
                else
                {
                    //Return internal server error
                    return OperationResult<UserDto?>.InternalServerError(message: GetIdentityErrors(roleResult.Errors));
                }
            }
            else
            {
                //return internal server error
                return OperationResult<UserDto?>.InternalServerError(message: GetIdentityErrors(createdUser.Errors));
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
