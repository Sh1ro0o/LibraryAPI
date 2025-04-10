using LibraryAPI.Model;

namespace LibraryAPI.Interface.Repository
{
    public interface IBookAuthorRepository
    {
        Task<List<BookAuthor>> GetByBookId(int bookId);
        Task AddRange(List<BookAuthor> bookAuthors);
        void DeleteRange(List<BookAuthor> bookAuthors);
    }
}
