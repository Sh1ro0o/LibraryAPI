using LibraryAPI.Common.Response;
using LibraryAPI.Dto.Genre;
using LibraryAPI.Filters;
using LibraryAPI.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("Genre")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [Authorize]
        [ProducesResponseType(200, Type = typeof(ResponseObject<IEnumerable<GenreDto>>))]
        [ProducesResponseType(500)]
        [HttpGet("allGenres")]
        public async Task<IActionResult> GetGenres([FromQuery] GenreFilter filter)
        {
            var result = await _genreService.GetAll(filter);

            return result.ToActionResult();
        }

        [Authorize]
        [ProducesResponseType(200, Type = typeof(ResponseObject<GenreDto>))] //OK
        [ProducesResponseType(409)] //Conflict
        [ProducesResponseType(404)] //NotFound
        [ProducesResponseType(400)] //Bad Request
        [ProducesResponseType(500)] //Internal Server Error
        [HttpPost]
        public async Task<IActionResult> CreateGenre([FromBody] CreateGenreDto model)
        {
            var result = await _genreService.CreateGenre(model);

            return result.ToActionResult();
        }

        [Authorize]
        [ProducesResponseType(200, Type = typeof(ResponseObject<GenreDto>))] //OK
        [ProducesResponseType(404)] //NotFound
        [ProducesResponseType(400)] //Bad Request
        [ProducesResponseType(500)] //Internal Server Error
        [HttpPut]
        public async Task<IActionResult> UpdateGenre([FromBody] SaveGenreDto model)
        {
            var result = await _genreService.UpdateGenre(model);

            return result.ToActionResult();
        }

        [Authorize]
        [ProducesResponseType(200, Type = typeof(ResponseObject<bool>))] //OK
        [ProducesResponseType(404)] //NotFound
        [ProducesResponseType(400)] //Bad Request
        [ProducesResponseType(500)] //Internal Server Error
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            var result = await _genreService.DeleteGenre(id);

            return result.ToActionResult();
        }
    }
}
