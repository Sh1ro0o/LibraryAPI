using LibraryAPI.Filters;
using LibraryAPI.Model;

namespace LibraryAPI.Interface.Repository
{
    public interface IBookGenreRepository
    {
        Task<ICollection<BookGenre>> GetByBookId(int bookId);
        Task AddRange(ICollection<BookGenre> bookGenres);
        void DeleteRange(ICollection<BookGenre> bookGenres);
    }
}
