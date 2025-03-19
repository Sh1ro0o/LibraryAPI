using LibraryAPI.Dto.Book;
using LibraryAPI.Filters;
using LibraryAPI.Interface.Service;
using LibraryAPI.Mapper;
using LibraryAPI.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Service
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<BookDto>> GetAll(BookFilter filter)
        {
            var books = await _unitOfWork.BookRepository.GetAll(filter);

            return books.Select(x => x.ToBookDto());
        }

        public async Task<BookDto> GetById(int id)
        {
            var book = await _unitOfWork.BookRepository.GetById(id);

            if (book == null)
            {
                
            }

            return book.ToBookDto();
        }
        
        public Task<BookDto> CreateBook(CreateBookDto model)
        {
            throw new NotImplementedException();
        }
        public Task<BookDto> UpdateBook(SaveBookDto model)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> DeleteBook(int id)
        {
            throw new NotImplementedException();
        }
    }
}
