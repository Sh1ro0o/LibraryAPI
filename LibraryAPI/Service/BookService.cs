using LibraryAPI.Common;
using LibraryAPI.Dto.Book;
using LibraryAPI.Filters;
using LibraryAPI.Interface.Service;
using LibraryAPI.Mapper;
using LibraryAPI.UnitOfWork;
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
                return OperationResult<BookDto?>.BadRequest(message: "Error! Please provide a book ISBN or Title.");
            }

            var existingBook = await _unitOfWork.BookRepository.GetOne(bookFilter);
            if(existingBook != null)
            {
                return OperationResult<BookDto?>.Conflict(message: "Error! Book already exists!");
            }

            //CHECK IF AUTHORS EXIST
            bool authorsExist = false;
            if (!model.AuthorIds.IsNullOrEmpty())
            {
                var existingAuthors = await _unitOfWork.AuthorRepository.CheckIfAuthorsExist(model.AuthorIds!);

                authorsExist = existingAuthors.Count == model.AuthorIds.Count;

                if (!authorsExist)
                {
                    return OperationResult<BookDto?>.NotFound(message: "Author Not Found!");
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
            var bookWithAuthors = await _unitOfWork.BookRepository.GetById(newBook.RecordId);

            if (bookWithAuthors == null)
            {
                return OperationResult<BookDto?>.InternalServerError(message: "Newly created Book not found!");
            }

            var bookDto = bookWithAuthors.ToBookDto();

            return OperationResult<BookDto?>.Success(bookDto);
        }
        public async Task<OperationResult<BookDto?>> UpdateBook(SaveBookDto model)
        {
            //Check if book exists
            var existingBook = await _unitOfWork.BookRepository.GetById(model.RecordId);

            if (existingBook == null)
            {
                return OperationResult<BookDto?>.NotFound(message: "Book Not Found!");
            }

            existingBook.PublishDate = model.PublishDate;
            existingBook.Title = model.Title;
            existingBook.PublishDate = model.PublishDate;
            existingBook.ISBN = model.ISBN;
            existingBook.Description = model.Description;

            _unitOfWork.BookRepository.Update(existingBook);
            await _unitOfWork.Commit();

            return OperationResult<BookDto?>.Success(existingBook.ToBookDto());
        }

        public async Task<OperationResult<bool>> DeleteBook(int id)
        {
            //Check if book exists
            var existingBook = await _unitOfWork.BookRepository.GetById(id);

            if (existingBook == null)
            {
                return OperationResult<bool>.NotFound(message: "Book Not Found!");
            }

            _unitOfWork.BookRepository.Delete(existingBook);
            await _unitOfWork.Commit();

            return OperationResult<bool>.Success(data: true);
        }
    }
}
