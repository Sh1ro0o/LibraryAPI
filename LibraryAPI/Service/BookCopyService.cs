using LibraryAPI.Common;
using LibraryAPI.Common.Response;
using LibraryAPI.Dto.BookCopy;
using LibraryAPI.Filters;
using LibraryAPI.Interface.Service;
using LibraryAPI.Interface.Utility;
using LibraryAPI.Mapper;
using LibraryAPI.UnitOfWork;

namespace LibraryAPI.Service
{
    public class BookCopyService : IBookCopyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISerialNumberGeneratorService _serialNumberGeneratorService;
        public BookCopyService(IUnitOfWork unitOfWork, ISerialNumberGeneratorService serialNumberGeneratorService)
        {
            _unitOfWork = unitOfWork;
            _serialNumberGeneratorService = serialNumberGeneratorService;
        }

        public async Task<OperationResult<IEnumerable<BookCopyDto>>> GetAll(BookCopyFilter filter)
        {
            var bookCopies = await _unitOfWork.BookCopyRepository.GetAll(filter);
            var bookCopiesDto = bookCopies.Select(x => x.ToBookCopyDto());

            return OperationResult<IEnumerable<BookCopyDto>>.Success(bookCopiesDto);
        }

        public async Task<OperationResult<BookCopyDto?>> GetById(int id)
        {
            var bookCopy = await _unitOfWork.BookCopyRepository.GetById(id);

            if (bookCopy == null)
            {
                return OperationResult<BookCopyDto?>.NotFound(message: $"BookCopy with Id: {id} not found!");
            }

            return OperationResult<BookCopyDto?>.Success(bookCopy.ToBookCopyDto());
        }

        public async Task<OperationResult<BookCopyDto?>> CreateBookCopy(CreateBookCopyDto model)
        {
            var newBookCopy = model.ToBookCopyFromCreateDto();

            newBookCopy.IsAvailable = true;
            newBookCopy.CreateDate = DateTime.UtcNow;
            newBookCopy.ModifiedDate = DateTime.UtcNow;
            newBookCopy.SerialNumber = _serialNumberGeneratorService.GenereateBookCopySerialNumber();

            await _unitOfWork.BookCopyRepository.Create(newBookCopy);
            await _unitOfWork.Commit();

            return OperationResult<BookCopyDto?>.Success(newBookCopy.ToBookCopyDto());
        }

        public async Task<OperationResult<BookCopyDto?>> UpdateBookCopy(SaveBookCopyDto model)
        {
            //Check if bookcopy exists
            var bookCopy = await _unitOfWork.BookCopyRepository.GetById(model.RecordId);

            if(bookCopy == null)
            {
                return OperationResult<BookCopyDto?>.NotFound(message: $"BookCopy with Id: {model.RecordId} not found!");
            }

            //Update book
            if (model.BookId is int bookId)
            {
                var existingBook = await _unitOfWork.BookRepository.GetById(bookId);
                if (existingBook == null)
                {
                    return OperationResult<BookCopyDto?>.NotFound(message: $"Book with Id: {bookId} not found!");
                }

                bookCopy.BookId = bookId;
            }
            if (model.IsAvailable is bool isAvailable)
            {
                bookCopy.IsAvailable = isAvailable;
            }
            bookCopy.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.BookCopyRepository.Update(bookCopy);
            await _unitOfWork.Commit();

            return OperationResult<BookCopyDto?>.Success(bookCopy.ToBookCopyDto());
        }

        public async Task<OperationResult<bool>> DeleteBookCopy(int id)
        {
            //Check if bookcopy exists
            var bookCopy = await _unitOfWork.BookCopyRepository.GetById(id);
            if (bookCopy == null)
            {
                return OperationResult<bool>.NotFound(message: $"BookCopy with Id: {id} not found!");
            }

            _unitOfWork.BookCopyRepository.Delete(bookCopy);
            await _unitOfWork.Commit();

            return OperationResult<bool>.Success(true);
        }
    }
}
