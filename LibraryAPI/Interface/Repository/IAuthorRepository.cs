using LibraryAPI.Common.Response;
using LibraryAPI.Filters;
using LibraryAPI.Model;

namespace LibraryAPI.Interface.Repository
{
    public interface IAuthorRepository
    {
        Task<PaginatedResponse<Author>> GetAll(AuthorFilter filter);
        Task<Author?> GetById(int id);
        Task<ICollection<Author>> CheckIfAuthorsExist(ICollection<int> authorIds);
        Task<Author> CreateAuthor(Author author);
        void UpdateAuthor(Author author);
        void DeleteAuthor(Author author);
    }
}
