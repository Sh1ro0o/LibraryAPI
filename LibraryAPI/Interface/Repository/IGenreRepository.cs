using LibraryAPI.Filters;
using LibraryAPI.Model;

namespace LibraryAPI.Interface.Repository
{
    public interface IGenreRepository
    {
        Task<List<Genre>> GetAll(GenreFilter filter);
        Task<Genre?> GetOne(GenreFilter filter);
        Task<Genre?> GetById(int id);
        Task<List<Genre>> CheckIfGenresExist(List<int> genreIds);
        Task<Genre> Create(Genre model);
        void Update(Genre book);
        void Delete(Genre book);
    }
}
