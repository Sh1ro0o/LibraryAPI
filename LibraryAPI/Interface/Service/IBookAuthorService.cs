using LibraryAPI.Common;
using LibraryAPI.Dto.BookAuthor;
using LibraryAPI.Model;

namespace LibraryAPI.Interface.Service
{
    public interface IBookAuthorService
    {
        Task<OperationResult<IEnumerable<BookAuthorDto>?>> UpdateBookAuthor(SaveBookAuthorDto model);
    }
}
