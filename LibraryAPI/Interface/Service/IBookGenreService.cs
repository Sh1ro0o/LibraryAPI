using LibraryAPI.Common;
using LibraryAPI.Dto.BookGenre;

namespace LibraryAPI.Interface.Service
{
    public interface IBookGenreService
    {
        Task<OperationResult<IEnumerable<BookGenreDto>?>> UpdateBookGenre(SaveBookGenreDto model);
    }
}
