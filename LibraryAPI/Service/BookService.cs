using LibraryAPI.Common;
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

        public async Task<OperationResult<IEnumerable<BookDto>>> GetAll(BookFilter filter)
        {
            var books = await _unitOfWork.BookRepository.GetAll(filter);

            var booksDto = books.Select(x => x.ToBookDto());
            
            return OperationResult<IEnumerable<BookDto>>.Success(booksDto);
        }

        public async Task<OperationResult<BookDto?>> GetById(int id)
        {
            var book = await _unitOfWork.BookRepository.GetById(id);

            if (book == null)
            {
                return OperationResult<BookDto?>.NotFound(message: $"Book with Id: {id} not found!");
            }

            return OperationResult<BookDto?>.Success(book.ToBookDto());
        }
        
        public async Task<OperationResult<BookDto?>> CreateBook(CreateBookDto model)
        {
            throw new NotImplementedException();

            var book = model.ToBookFromCreateDto();

            //Check if book exists
            //TO-DO: Check for Author, ISBN and then Title if they exist so create If...
            BookFilter filter = new BookFilter
            { 
                ISBN = model.ISBN
            };
            var existingBook = await _unitOfWork.BookRepository.GetAll(filter);

            if (existingBook != null)
            {
                return OperationResult<BookDto?>.Conflict($"Book with ISBN: {model.ISBN}");
            }
        }
        public Task<OperationResult<BookDto?>> UpdateBook(SaveBookDto model)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<bool>> DeleteBook(int id)
        {
            throw new NotImplementedException();
        }
    }
}
