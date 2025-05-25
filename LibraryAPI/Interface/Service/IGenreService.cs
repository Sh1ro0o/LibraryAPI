using LibraryAPI.Common;
using LibraryAPI.Dto.Genre;
using LibraryAPI.Filters;

namespace LibraryAPI.Interface.Service
{
    public interface IGenreService
    {
        Task<OperationResult<IEnumerable<GenreDto>>> GetAll(GenreFilter filter);
        Task<OperationResult<GenreDto?>> GetById(int id);
        Task<OperationResult<GenreDto?>> CreateGenre(CreateGenreDto model);
        Task<OperationResult<GenreDto?>> UpdateGenre(SaveGenreDto model);
        Task<OperationResult<bool>> DeleteGenre(int id);
    }
}
