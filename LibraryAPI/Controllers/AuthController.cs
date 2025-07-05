using LibraryAPI.Common.Constants;
using LibraryAPI.Common.Response;
using LibraryAPI.Dto.Author;
using LibraryAPI.Dto.User;
using LibraryAPI.Filters;
using LibraryAPI.Interface.Service;
using LibraryAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ICookieService _cookieService;
        public AuthController(IAuthService authService, ICookieService cookieService)
        {
            _authService = authService;
            _cookieService = cookieService;
        }

        [ProducesResponseType(200, Type = typeof(ResponseObject<UserAuthenticationDto?>))] //OK
        [ProducesResponseType(400)] //Bad Request
        [ProducesResponseType(404)] //Not Found
        [ProducesResponseType(500)] //Internal Server Error
        [HttpPost("Refresh")]
        public async Task<IActionResult> RefreshSessionToken()
        {
            var refreshToken = HttpContext.Request.Cookies[CookieNames.RefreshToken];
            if (refreshToken == null) {
                return BadRequest();
            }

            var result = await _authService.RefreshSessionToken(refreshToken);

            if (result.Data != null && result.IsSuccessful)
            {
                _cookieService.CreateSessionCookie(result.Data.Token, Request, Response);
            }

            return result.ToActionResult();
        }
    }
}
