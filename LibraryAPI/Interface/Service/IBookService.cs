using LibraryAPI.Common;
using LibraryAPI.Dto.Book;
using LibraryAPI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Interface.Service
{
    public interface IBookService
    {
        Task<OperationResult<IEnumerable<BookDto>>> GetAll(BookFilter filter);
        Task<OperationResult> GetById(int id);
        Task<OperationResult> CreateBook(CreateBookDto model);
        Task<OperationResult> UpdateBook(SaveBookDto model);
        Task<OperationResult> DeleteBook(int id);
    }
}
