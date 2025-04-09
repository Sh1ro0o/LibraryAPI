using LibraryAPI.Model;

namespace LibraryAPI.Interface.Repository
{
    public interface IBookGenreRepository
    {
        Task<List<BookGenre>> CreateBookGenreConnections(Book book, List<int> genreIds);
    }
}
