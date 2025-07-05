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

        public UserController (IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [ProducesResponseType(200, Type = typeof(ResponseObject<UserAuthenticationDto?>))] //OK
        [ProducesResponseType(400)] //Bad Request
        [ProducesResponseType(500)] //Internal Server Error
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var result = await _userService.UserRegister(model);

            //If successful create SessionCookie
            if (result.IsSuccessful && result.Data != null)
            {
                CreateSessionCookie(result.Data.Token);
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

            //If successful create SessionCookie
            if (result.IsSuccessful && result.Data != null)
            {
                CreateSessionCookie(result.Data.Token);
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


        private void CreateSessionCookie(string token)
        {
            var requestHost = HttpContext.Request.Host.Host?.ToLowerInvariant();
            var isLocalhost = string.IsNullOrEmpty(requestHost) || requestHost.Contains("localhost");
            string? cookieDomain = null;

            if (!isLocalhost)
            {
                cookieDomain = requestHost;
            }

            Response.Cookies.Append(CookieNames.SessionToken, token, new CookieOptions
            {
                HttpOnly = true,
                Secure = !isLocalhost,
                SameSite = isLocalhost ? SameSiteMode.Lax : SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddDays(7),
                Domain = cookieDomain,
                Path = "/"
            });
        }
    }
}
