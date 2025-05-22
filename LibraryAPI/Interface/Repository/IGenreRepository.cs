using LibraryAPI.Common.Response;
using LibraryAPI.Filters;
using LibraryAPI.Model;

namespace LibraryAPI.Interface.Repository
{
    public interface IGenreRepository
    {
        Task<PaginatedResponse<Genre>> GetAll(GenreFilter filter);
        Task<Genre?> GetOne(GenreFilter filter);
        Task<Genre?> GetById(int id);
        Task<ICollection<Genre>> CheckIfGenresExist(List<int> genreIds);
        Task<Genre> Create(Genre model);
        void Update(Genre book);
        void Delete(Genre book);
    }
}
