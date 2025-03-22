using LibraryAPI.Common.Response;
using LibraryAPI.Dto.Book;
using LibraryAPI.Filters;
using LibraryAPI.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("Book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [ProducesResponseType(200, Type = typeof(ResponseObject<IEnumerable<BookDto>>))]
        [ProducesResponseType(500)]
        [HttpGet("allBooks")]
        public async Task<IActionResult> GetBooks([FromQuery] BookFilter filter)
        {
            var result = await _bookService.GetAll(filter);

            return result.ToActionResult();
        }

        [ProducesResponseType(200, Type = typeof(ResponseObject<BookDto>))] //OK
        [ProducesResponseType(409)] //Conflict
        [ProducesResponseType(404)] //NotFound
        [ProducesResponseType(400)] //Bad Request
        [ProducesResponseType(500)] //Internal Server Error
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody]CreateBookDto model)
        {
            var result = await _bookService.CreateBook(model);

            return result.ToActionResult();
        }
    }
}
