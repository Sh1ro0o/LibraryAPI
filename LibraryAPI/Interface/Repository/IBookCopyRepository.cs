using LibraryAPI.Dto.Book;
using LibraryAPI.Dto.BookCopy;
using LibraryAPI.Filters;
using LibraryAPI.Model;

namespace LibraryAPI.Interface.Repository
{
    public interface IBookCopyRepository
    {
        Task<List<BookCopy>> GetAll(BookCopyFilter filter);
        Task<BookCopy?> GetOne(BookCopyFilter filter);
        Task<BookCopy?> GetById(int id);
        Task<BookCopy> Create(BookCopy model);
        void Update(BookCopy model);
        void Delete(BookCopy id);
    }
}
