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
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signinManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signinManager;
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
                    var tokenDto = await _tokenService.CreateToken(newUser);

                    var newUserDto = new UserDto
                    {
                        Email = model.Email,
                        Token = tokenDto.Token,
                        ExpiresOn = tokenDto.ExpiresOn
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

        public async Task<OperationResult<UserDto?>> UserLogin(LoginDto model)
        {
            //Check if user with Email exists
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return OperationResult<UserDto?>.BadRequest(message: "Invalid email or password.");
            }

            //Try to login
            var userLogin = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (userLogin.Succeeded)
            {
                var tokenDto = await _tokenService.CreateToken(user);

                var userDto = new UserDto
                {
                    Email = model.Email,
                    Token = tokenDto.Token,
                    ExpiresOn = tokenDto.ExpiresOn
                };

                return OperationResult<UserDto?>.Success(userDto);
            }

            return OperationResult<UserDto?>.BadRequest(message: "Invalid email or password.");
        }

        #region PRIVATE Methods

        private string GetIdentityErrors(IEnumerable<IdentityError> errors)
        {
            return string.Join(", ", errors.Select(e => e.Description));
        }

        #endregion
    }
}
