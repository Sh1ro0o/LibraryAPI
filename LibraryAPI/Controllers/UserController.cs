using LibraryAPI.Common.Response;
using LibraryAPI.Dto.BookGenre;
using LibraryAPI.Dto.User;
using LibraryAPI.Interface.Service;
using LibraryAPI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController (IUserService userService)
        {
            _userService = userService;
        }

        [ProducesResponseType(200, Type = typeof(ResponseObject<bool>))] //OK
        [ProducesResponseType(400)] //Bad Request
        [ProducesResponseType(500)] //Internal Server Error
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var result = await _userService.UserRegister(model);

            return result.ToActionResult();
        }
    }
}
