using LibraryAPI.Common.Constants;
using LibraryAPI.Common.Response;
using LibraryAPI.Dto.User;
using LibraryAPI.Filters;
using LibraryAPI.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly ICookieService _cookieService;

        public UserController (IUserService userService, IConfiguration configuration, ICookieService cookieService)
        {
            _userService = userService;
            _configuration = configuration;
            _cookieService = cookieService;
        }

        [ProducesResponseType(200, Type = typeof(ResponseObject<UserAuthenticationDto?>))] //OK
        [ProducesResponseType(400)] //Bad Request
        [ProducesResponseType(500)] //Internal Server Error
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var result = await _userService.UserRegister(model);

            //If successful create SessionCookie and RefreshCookie
            if (result.IsSuccessful && result.Data != null)
            {
                _cookieService.CreateSessionCookie(result.Data.Token, Request, Response);
                _cookieService.CreateRefreshCookie(result.Data.RefreshToken, Request, Response);
            }

            return result.ToActionResult();
        }

        [ProducesResponseType(200, Type = typeof(ResponseObject<UserAuthenticationDto?>))] //OK
        [ProducesResponseType(400)] //Bad Request
        [ProducesResponseType(500)] //Internal Server Error
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _userService.UserLogin(model);

            //If successful create SessionCookie and RefreshCookie
            if (result.IsSuccessful && result.Data != null)
            {
                _cookieService.CreateSessionCookie(result.Data.Token, Request, Response);
                _cookieService.CreateRefreshCookie(result.Data.RefreshToken, Request, Response);
            }

            return result.ToActionResult();
        }

        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(200, Type = typeof(ResponseObject<IEnumerable<UserDto?>>))] //OK
        [ProducesResponseType(401)] //Unauthorized
        [ProducesResponseType(500)] //Internal Server Error
        [HttpGet("allUsers")]
        public async Task<IActionResult> GetUsers([FromQuery] UserFilter filter)
        {
            var result = await _userService.GetAll(filter);

            return result.ToActionResult();
        }

        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(200, Type = typeof(ResponseObject<UserDto?>))] //OK
        [ProducesResponseType(401)] //Unauthorized
        [ProducesResponseType(404)] //Not Found
        [ProducesResponseType(500)] //Internal Server Error
        [HttpGet("getByEmail")]
        public async Task<IActionResult> GetByEmail([FromQuery] string email)
        {
            var result = await _userService.GetByEmail(email);

            return result.ToActionResult();
        }
    }
}
