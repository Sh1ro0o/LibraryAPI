using LibraryAPI.Common;
using LibraryAPI.Common.Constants;
using LibraryAPI.Dto.Book;
using LibraryAPI.Dto.User;
using LibraryAPI.Filters;
using LibraryAPI.Interface.Service;
using LibraryAPI.Mapper;
using LibraryAPI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<OperationResult<UserAuthenticationDto?>> UserRegister(RegisterDto model)
        {
            //Check if user with Email already exists
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return OperationResult<UserAuthenticationDto?>.Conflict(message: "A user with this email already exists.");
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

                    var newUserDto = new UserAuthenticationDto
                    {
                        Email = model.Email,
                        Token = tokenDto.Token,
                        ExpiresOn = tokenDto.ExpiresOn,
                        Roles = tokenDto.Roles,
                    };

                    //Return success
                    return OperationResult<UserAuthenticationDto?>.Success(newUserDto);
                }
                else
                {
                    //Return internal server error
                    return OperationResult<UserAuthenticationDto?>.InternalServerError(message: GetIdentityErrors(roleResult.Errors));
                }
            }
            else
            {
                //return internal server error
                return OperationResult<UserAuthenticationDto?>.InternalServerError(message: GetIdentityErrors(createdUser.Errors));
            }
        }

        public async Task<OperationResult<UserAuthenticationDto?>> UserLogin(LoginDto model)
        {
            //Check if user with Email exists
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return OperationResult<UserAuthenticationDto?>.BadRequest(message: "Invalid email or password.");
            }

            //Try to login
            var userLogin = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (userLogin.Succeeded)
            {
                var tokenDto = await _tokenService.CreateToken(user);

                var userDto = new UserAuthenticationDto
                {
                    Email = model.Email,
                    Token = tokenDto.Token,
                    ExpiresOn = tokenDto.ExpiresOn,
                    Roles = tokenDto.Roles,
                };

                return OperationResult<UserAuthenticationDto?>.Success(userDto);
            }

            return OperationResult<UserAuthenticationDto?>.BadRequest(message: "Invalid email or password.");
        }

        public async Task<OperationResult<IEnumerable<UserDto?>>> GetAll(UserFilter userFilter)
        {
            var usersQuerry = _userManager.Users;
            var totalCout = await usersQuerry.CountAsync();

            if (!string.IsNullOrWhiteSpace(userFilter.Id))
            {
                usersQuerry = usersQuerry.Where(x => x.Id == userFilter.Id);
            }

            if (!string.IsNullOrWhiteSpace(userFilter.Email))
            {
                usersQuerry = usersQuerry.Where(x => x.Email == userFilter.Email);
            }

            if (userFilter.PageSize != null && userFilter.PageNumber != null)
            {
                int skip = (userFilter.PageNumber.Value - 1) * userFilter.PageSize.Value;
                usersQuerry = usersQuerry.Skip(skip).Take(userFilter.PageSize.Value);
            }

            var users = await usersQuerry.ToListAsync();
            var usersDto = users.Select(x => x.ToUserDto());

            return OperationResult<IEnumerable<UserDto?>>.Success(data: usersDto, totalCount: totalCout);
        }


        public async Task<OperationResult<UserDto?>> GetByEmail(string email)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser == null)
            {
                return OperationResult<UserDto?>.NotFound(message: $"User with Email: {email} not found!");
            }

            return OperationResult<UserDto?>.Success(existingUser.ToUserDto());
        }


        #region PRIVATE Methods

        private string GetIdentityErrors(IEnumerable<IdentityError> errors)
        {
            return string.Join(", ", errors.Select(e => e.Description));
        }

        #endregion
    }
}
