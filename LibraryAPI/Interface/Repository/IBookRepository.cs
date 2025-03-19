using LibraryAPI.Dto;
using LibraryAPI.Filters;
using LibraryAPI.Model;

namespace LibraryAPI.Interface
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAll(BookFilter filter);
        Task<Book?> GetById(int id);
        Task<Book> Create(Book model);
        void Update(Book book);
        void Delete(Book book);
    }
}
