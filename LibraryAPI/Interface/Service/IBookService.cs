using LibraryAPI.Common;
using LibraryAPI.Dto.Book;
using LibraryAPI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Interface.Service
{
    public interface IBookService
    {
        Task<OperationResult<IEnumerable<BookDto>>> GetAll(BookFilter filter);
        Task<OperationResult<BookDto?>> GetById(int id);
        Task<OperationResult<BookDto?>> CreateBook(CreateBookDto model);
        Task<OperationResult<BookDto?>> UpdateBook(SaveBookDto model);
        Task<OperationResult<bool>> DeleteBook(int id);
    }
}
