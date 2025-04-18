using LibraryAPI.Model;

namespace LibraryAPI.Interface.Repository
{
    public interface IBookAuthorRepository
    {
        Task<ICollection<BookAuthor>> GetByBookId(int bookId);
        Task AddRange(ICollection<BookAuthor> bookAuthors);
        void DeleteRange(ICollection<BookAuthor> bookAuthors);
    }
}
