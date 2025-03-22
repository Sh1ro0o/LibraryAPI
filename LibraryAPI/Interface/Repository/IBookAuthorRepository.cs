using LibraryAPI.Model;

namespace LibraryAPI.Interface.Repository
{
    public interface IBookAuthorRepository
    {
        Task<List<BookAuthor>> CreateBookAuthorConnections(Book book, List<int> authorIds);
    }
}
