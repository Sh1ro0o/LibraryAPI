using LibraryAPI.Common.Response;
using LibraryAPI.Dto.Book;
using LibraryAPI.Filters;
using LibraryAPI.Interface.Service;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("Author")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [ProducesResponseType(200, Type = typeof(ResponseObject<IEnumerable<BookDto>>))]
        [ProducesResponseType(500)]
        [HttpGet("allAuthors")]
        public async Task<IActionResult> GetAllAuthors([FromQuery] AuthorFilter filter)
        {
            var authors = await _authorService.GetAll(filter);

            return authors.ToActionResult();
        }
    }
}
