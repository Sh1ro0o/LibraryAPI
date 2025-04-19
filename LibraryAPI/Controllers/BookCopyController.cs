using LibraryAPI.Common.Response;
using LibraryAPI.Dto.Book;
using LibraryAPI.Dto.BookCopy;
using LibraryAPI.Filters;
using LibraryAPI.Interface.Service;
using LibraryAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("BookCopy")]
    [ApiController]
    public class BookCopyController : ControllerBase
    {
        private readonly IBookCopyService _bookCopyService;
        public BookCopyController(IBookCopyService bookCopyService)
        {
            _bookCopyService = bookCopyService;
        }

        [Authorize]
        [ProducesResponseType(200, Type = typeof(ResponseObject<IEnumerable<BookCopyDto>>))]
        [ProducesResponseType(500)]
        [HttpGet("allBooksCopies")]
        public async Task<IActionResult> GetBookCopies([FromQuery] BookCopyFilter filter)
        {
            var bookCopies = await _bookCopyService.GetAll(filter);

            return bookCopies.ToActionResult();
        }

        [Authorize]
        [ProducesResponseType(200, Type = typeof(ResponseObject<BookCopyDto>))] //OK
        [ProducesResponseType(409)] //Conflict
        [ProducesResponseType(404)] //NotFound
        [ProducesResponseType(400)] //Bad Request
        [ProducesResponseType(500)] //Internal Server Error
        [HttpPost]
        public async Task<IActionResult> CreateBookCopy([FromBody] CreateBookCopyDto model)
        {
            var result = await _bookCopyService.CreateBookCopy(model);

            return result.ToActionResult();
        }

        [Authorize]
        [ProducesResponseType(200, Type = typeof(ResponseObject<BookCopyDto>))] //OK
        [ProducesResponseType(404)] //NotFound
        [ProducesResponseType(400)] //Bad Request
        [ProducesResponseType(500)] //Internal Server Error
        [HttpPut]
        public async Task<IActionResult> UpdateBookCopy([FromBody] SaveBookCopyDto model)
        {
            var result = await _bookCopyService.UpdateBookCopy(model);

            return result.ToActionResult();
        }

        [Authorize]
        [ProducesResponseType(200, Type = typeof(ResponseObject<bool>))] //OK
        [ProducesResponseType(404)] //NotFound
        [ProducesResponseType(400)] //Bad Request
        [ProducesResponseType(500)] //Internal Server Error
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookCopy([FromRoute] int id)
        {
            var result = await _bookCopyService.DeleteBookCopy(id);

            return result.ToActionResult();
        }
    }
}
