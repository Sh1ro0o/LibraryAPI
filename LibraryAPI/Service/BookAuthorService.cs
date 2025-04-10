using LibraryAPI.Common;
using LibraryAPI.Dto.Book;
using LibraryAPI.Dto.BookAuthor;
using LibraryAPI.Interface.Service;
using LibraryAPI.Mapper;
using LibraryAPI.Model;
using LibraryAPI.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Service
{
    public class BookAuthorService : IBookAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BookAuthorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<IEnumerable<BookAuthorDto>?>> UpdateBookAuthor(SaveBookAuthorDto model)
        {
            //Check if bookId exists
            var existingBook = await _unitOfWork.BookRepository.GetById(model.BookId);
            if (existingBook == null)
            {
                return OperationResult<IEnumerable<BookAuthorDto>?>.NotFound(message: $"Book with Id: {model.BookId} not found!");
            }

            //Check if authors exist
            var existingAuthors = await _unitOfWork.AuthorRepository.CheckIfAuthorsExist(model.AuthorIds);
            bool authorsExist = existingAuthors.Count == model.AuthorIds.Count;
            if (!authorsExist)
            {
                return OperationResult<IEnumerable<BookAuthorDto>?>.NotFound(message: "Author Not Found!");
            }

            //Get existing connections
            var existingBookAuthors = await _unitOfWork.BookAuthorRepository.GetByBookId(model.BookId);
            var existingBookAuthorIds = existingBookAuthors.Select(x => x.AuthorId).ToList();

            //BookAuthors to add
            var bookAuthorsToAdd = model.AuthorIds
                .Where(x => !existingBookAuthorIds.Contains(x))
                .Select(x => new BookAuthor
                {
                    BookId = model.BookId,
                    AuthorId = x
                }).ToList();

            //BookAuthors to delete
            var bookAuthorsToDelete = existingBookAuthors
                .Where(x => !model.AuthorIds.Contains(x.AuthorId))
                .ToList();

            await _unitOfWork.BookAuthorRepository.AddRange(bookAuthorsToAdd);
            _unitOfWork.BookAuthorRepository.DeleteRange(bookAuthorsToDelete);

            await _unitOfWork.Commit();

            //Return new connections
            var result = await _unitOfWork.BookAuthorRepository.GetByBookId(model.BookId);
            var bookAuthorDtos = result.Select(x => x.ToBookAuthorDto());
            return OperationResult<IEnumerable<BookAuthorDto>?>.Success(bookAuthorDtos);
        }
    }
}
