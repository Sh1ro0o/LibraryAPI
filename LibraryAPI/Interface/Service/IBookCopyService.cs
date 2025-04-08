using LibraryAPI.Common;
using LibraryAPI.Common.Response;
using LibraryAPI.Dto.BookCopy;
using LibraryAPI.Filters;

namespace LibraryAPI.Interface.Service
{
    public interface IBookCopyService
    {
        Task<OperationResult<IEnumerable<BookCopyDto>>> GetAll(BookCopyFilter filter);
        Task<OperationResult<BookCopyDto?>> GetById(int id);
        Task<OperationResult<BookCopyDto?>> CreateBookCopy(CreateBookCopyDto model);
        Task<OperationResult<BookCopyDto?>> UpdateBookCopy(SaveBookCopyDto model);
        Task<OperationResult<bool>> DeleteBookCopy(int id);
    }
}
