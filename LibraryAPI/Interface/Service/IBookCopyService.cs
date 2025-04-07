using LibraryAPI.Common.Response;
using LibraryAPI.Dto.BookCopy;
using LibraryAPI.Filters;

namespace LibraryAPI.Interface.Service
{
    public interface IBookCopyService
    {
        Task<ResponseObject<IEnumerable<BookCopyDto>>> GetAll(BookCopyFilter filter);
        Task<ResponseObject<BookCopyDto?>> GetById(BookCopyFilter filter);
        Task<ResponseObject<BookCopyDto?>> CreateBookCopy(CreateBookCopyDto model);
        Task<ResponseObject<BookCopyDto?>> SaveBookCopy(SaveBookCopyDto model);
        Task<ResponseObject<bool>> DeleteBookCopy(int id);
    }
}
