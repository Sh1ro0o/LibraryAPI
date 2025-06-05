using LibraryAPI.Common.Constants;
using LibraryAPI.Common.Response;
using LibraryAPI.Dto.Author;
using LibraryAPI.Dto.Book;
using LibraryAPI.Filters;
using LibraryAPI.Interface.Service;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [ProducesResponseType(200, Type = typeof(ResponseObject<IEnumerable<AuthorDto>>))]
        [ProducesResponseType(500)]
        [HttpGet("allAuthors")]
        public async Task<IActionResult> GetAllAuthors([FromQuery] AuthorFilter filter)
        {
            var authors = await _authorService.GetAll(filter);

            return authors.ToActionResult();
        }

        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(200, Type = typeof(ResponseObject<AuthorDto>))] //OK
        [ProducesResponseType(404)] //NotFound
        [ProducesResponseType(400)] //Bad Request
        [ProducesResponseType(500)] //Internal Server Error
        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorDto model)
        {
            var author = await _authorService.CreateAuthor(model);

            return author.ToActionResult();
        }

        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(200, Type = typeof(ResponseObject<AuthorDto>))] //OK
        [ProducesResponseType(404)] //NotFound
        [ProducesResponseType(400)] //Bad Request
        [ProducesResponseType(500)] //Internal Server Error
        [HttpPut]
        public async Task<IActionResult> UpdateAuthor([FromBody] SaveAuthorDto model)
        {
            var author = await _authorService.UpdateAuthor(model);

            return author.ToActionResult();
        }

        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(200, Type = typeof(ResponseObject<bool>))] //OK
        [ProducesResponseType(404)] //NotFound
        [ProducesResponseType(400)] //Bad Request
        [ProducesResponseType(500)] //Internal Server Error
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor([FromRoute] int id)
        {
            var result = await _authorService.DeleteAuthor(id);

            return result.ToActionResult();
        }
    }
}
