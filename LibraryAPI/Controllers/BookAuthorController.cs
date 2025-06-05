using LibraryAPI.Common.Constants;
using LibraryAPI.Common.Response;
using LibraryAPI.Dto.Author;
using LibraryAPI.Dto.BookAuthor;
using LibraryAPI.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("BookAuthor")]
    [ApiController]
    public class BookAuthorController : ControllerBase
    {
        private readonly IBookAuthorService _bookAuthorService;
        public BookAuthorController(IBookAuthorService bookAuthorService)
        {
            _bookAuthorService = bookAuthorService;
        }

        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(200, Type = typeof(ResponseObject<List<BookAuthorDto>>))] //OK
        [ProducesResponseType(404)] //NotFound
        [ProducesResponseType(400)] //Bad Request
        [ProducesResponseType(500)] //Internal Server Error
        [HttpPut]
        public async Task<IActionResult> UpdateBookAuthor([FromBody] SaveBookAuthorDto model)
        {
            var author = await _bookAuthorService.UpdateBookAuthor(model);

            return author.ToActionResult();
        }
    }
}
