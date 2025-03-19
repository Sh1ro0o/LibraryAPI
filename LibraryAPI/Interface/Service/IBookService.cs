using LibraryAPI.Dto.Book;
using LibraryAPI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Interface.Service
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAll(BookFilter filter);
        Task<BookDto> GetById(int id);
        Task<BookDto> CreateBook(CreateBookDto model);
        Task<BookDto> UpdateBook(SaveBookDto model);
        Task<IActionResult> DeleteBook(int id);
    }
}
