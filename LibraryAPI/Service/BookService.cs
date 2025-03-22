using LibraryAPI.Common;
using LibraryAPI.Dto.Book;
using LibraryAPI.Filters;
using LibraryAPI.Interface.Service;
using LibraryAPI.Mapper;
using LibraryAPI.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
            var books = await _unitOfWork.BookRepository.GetAll(filter).ToListAsync();

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
            //CHECK IF BOOK EXISTS
            var bookFilter = new BookFilter();

            if (!string.IsNullOrEmpty(model.ISBN)) //Check if exists by ISBN or by Title
            {
                bookFilter.ISBN = model.ISBN;
            }
            else if (!string.IsNullOrEmpty(model.Title))
            {
                bookFilter.Title = model.Title;
            }
            else
            {
                return OperationResult<BookDto?>.BadRequest("Error! Please provide a book ISBN or Title.");
            }

            var existingBook = await _unitOfWork.BookRepository.GetAll(bookFilter).FirstOrDefaultAsync();
            if(existingBook != null)
            {
                return OperationResult<BookDto?>.Conflict("Error! Book already exists!");
            }

            //CHECK IF AUTHORS EXIST
            bool authorsExist = false;
            if (!model.AuthorIds.IsNullOrEmpty())
            {
                var existingAuthors = await _unitOfWork.AuthorRepository
                    .GetAll(new AuthorFilter())
                    .Where(x => model.AuthorIds.Contains(x.RecordId))
                    .Distinct()
                    .ToListAsync();

                authorsExist = existingAuthors.Count == model.AuthorIds.Count;

                if (!authorsExist)
                {
                    return OperationResult<BookDto?>.NotFound("Author Not Found!");
                }
            }

            //CREATE BOOK
            var newBook = await _unitOfWork.BookRepository.Create(model.ToBookFromCreateDto());

            //CREATE BOOKAUTHOR CONNECTIONS
            if (authorsExist)
            {
                var bookAuthors = await _unitOfWork.BookAuthorRepository.CreateBookAuthorConnections(newBook, model.AuthorIds);
            }

            await _unitOfWork.Commit();

            //RETURN NEWLY CREATED BOOK WITH AUTHORS
            var bookWithAuthors = await _unitOfWork.BookRepository.GetAll(new BookFilter()
            {
                RecordId = newBook.RecordId
            }).FirstOrDefaultAsync();

            if (bookWithAuthors == null)
            {
                return OperationResult<BookDto?>.InternalServerError("Newly created Book not found!");
            }

            var bookDto = bookWithAuthors.ToBookDto();

            return OperationResult<BookDto?>.Success(bookDto);
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
