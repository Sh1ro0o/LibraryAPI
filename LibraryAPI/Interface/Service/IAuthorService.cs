using LibraryAPI.Common;
using LibraryAPI.Dto.Author;
using LibraryAPI.Filters;

namespace LibraryAPI.Interface.Service
{
    public interface IAuthorService
    {
        Task<OperationResult<IEnumerable<AuthorDto>>> GetAll(AuthorFilter authorFilter);
        Task<OperationResult<AuthorDto?>> CreateAuthor(CreateAuthorDto model);
        Task<OperationResult<AuthorDto?>> UpdateAuthor(SaveAuthorDto model);
        Task<OperationResult<bool>> DeleteAuthor(int id);
    }
}
