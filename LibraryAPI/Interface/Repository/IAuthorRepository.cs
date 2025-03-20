using LibraryAPI.Filters;
using LibraryAPI.Model;

namespace LibraryAPI.Interface.Repository
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAll(AuthorFilter filter);
        Task<Author?> GetById(int id);
        Task<Author> CreateAuthor(Author author);
        void UpdateAuthor(Author author);
        void DeleteAuthor(Author author);
    }
}
