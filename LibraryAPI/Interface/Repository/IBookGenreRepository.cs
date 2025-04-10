using LibraryAPI.Model;

namespace LibraryAPI.Interface.Repository
{
    public interface IBookGenreRepository
    {
        Task<List<BookGenre>> GetByBookId(int bookId);
        Task AddRange(List<BookGenre> bookGenres);
        void DeleteRange(List<BookGenre> bookGenres);
    }
}
