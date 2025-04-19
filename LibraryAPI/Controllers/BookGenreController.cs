using LibraryAPI.Common.Response;
using LibraryAPI.Dto.BookGenre;
using LibraryAPI.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("BookGenre")]
    [ApiController]
    public class BookGenreController : ControllerBase
    {
        private readonly IBookGenreService _bookGenreService;
        public BookGenreController(IBookGenreService bookGenreService)
        {
            _bookGenreService = bookGenreService;
        }

        [Authorize]
        [ProducesResponseType(200, Type = typeof(ResponseObject<List<BookGenreDto>>))] //OK
        [ProducesResponseType(404)] //NotFound
        [ProducesResponseType(400)] //Bad Request
        [ProducesResponseType(500)] //Internal Server Error
        [HttpPut]
        public async Task<IActionResult> UpdateBookGenre([FromBody] SaveBookGenreDto model)
        {
            var author = await _bookGenreService.UpdateBookGenre(model);

            return author.ToActionResult();
        }
    }
}
