using FakeItEasy;
using FluentAssertions;
using LibraryAPI.Common.Enums;
using LibraryAPI.Dto.Book;
using LibraryAPI.Filters;
using LibraryAPI.Interface.Service;
using LibraryAPI.Model;
using LibraryAPI.Service;
using LibraryAPI.UnitOfWork;
using System.Collections;
using System.Collections.ObjectModel;

namespace LibraryAPI.Tests.Services
{
    public class BookServiceTests
    {
        private readonly IBookService _bookService;
        private readonly IUnitOfWork _unitOfWork;
        public BookServiceTests()
        {
            //Dependencies
            var fakeUnitOfWork = A.Fake<IUnitOfWork>();
            _unitOfWork = fakeUnitOfWork;

            //SUT
            _bookService = new BookService(_unitOfWork);
        }

        [Fact]
        public async Task BookService_GetAll_ReturnsSuccess()
        {
            //Arrange
            var filter = new BookFilter();
            var fakeBooks = A.Fake<List<Book>>();

            A.CallTo(() => _unitOfWork.BookRepository.GetAll(filter)).Returns(fakeBooks);

            //Act
            var result = await _bookService.GetAll(filter);

            //Assert
            result.IsSuccessful.Should().BeTrue();
            result.ErrorType.Should().BeNull();
            result.Data.Should().NotBeNull();
            result.Data.Should().BeAssignableTo<IEnumerable<BookDto>>();
        }

        [Fact]
        public async Task BookService_CreateBook_ReturnsConflict()
        {
            //Arrange
            var createBookDto = new CreateBookDto { Title = "TEST-TITLE" };
            var fakeExistingBook = A.Fake<Book>();
            var expectedErrorType = OperationErrorType.Conflict;

            A.CallTo(() => _unitOfWork.BookRepository.GetOne(A<BookFilter>._)).Returns(fakeExistingBook);

            //Act
            var result = await _bookService.CreateBook(createBookDto);

            //Assert
            result.IsSuccessful.Should().BeFalse();
            result.ErrorType.Should().Be(expectedErrorType);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task BookService_CreateBook_ReturnsAuthorNotFound()
        {
            //Arrange
            var createBookDto = new CreateBookDto { Title = "TEST-TITLE", AuthorIds = [1, 5] };
            var fakeExistingBook = A.Fake<Book>();
            var expectedErrorType = OperationErrorType.NotFound;
            var existingAuthors = GetValidAuthors();

            A.CallTo(() => _unitOfWork.BookRepository.GetOne(A<BookFilter>._)).Returns(Task.FromResult<Book?>(null));
            A.CallTo(() => _unitOfWork.AuthorRepository.CheckIfAuthorsExist(createBookDto.AuthorIds)).Returns(existingAuthors);

            //Act
            var result = await _bookService.CreateBook(createBookDto);

            //Assert
            result.IsSuccessful.Should().BeFalse();
            result.ErrorType.Should().Be(expectedErrorType);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task BookService_CreateBook_ReturnsGenreNotFound()
        {
            //Arrange
            var createBookDto = new CreateBookDto { 
                Title = "TEST-TITLE", 
                AuthorIds = [1],
                GenreIds = [1, 5]
            };
            var fakeExistingBook = A.Fake<Book>();
            var expectedErrorType = OperationErrorType.NotFound;
            var existingAuthors = GetValidAuthors();
            var existingGenres = GetValidGenres();

            A.CallTo(() => _unitOfWork.BookRepository.GetOne(A<BookFilter>._)).Returns(Task.FromResult<Book?>(null));
            A.CallTo(() => _unitOfWork.AuthorRepository.CheckIfAuthorsExist(createBookDto.AuthorIds)).Returns(existingAuthors);
            A.CallTo(() => _unitOfWork.GenreRepository.CheckIfGenresExist(createBookDto.GenreIds)).Returns(existingGenres);

            //Act
            var result = await _bookService.CreateBook(createBookDto);

            //Assert
            result.IsSuccessful.Should().BeFalse();
            result.ErrorType.Should().Be(expectedErrorType);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task BookService_CreateBook_ReturnsInternalServerError()
        {
            //Arrange
            var createBookDto = new CreateBookDto
            {
                Title = "TEST-TITLE",
                AuthorIds = [1],
                GenreIds = [1]
            };
            var fakeExistingBook = A.Fake<Book>();
            var expectedErrorType = OperationErrorType.InternalServerError;
            var existingAuthors = GetValidAuthors();
            var existingGenres = GetValidGenres();

            var createdBook = new Book
            { 
                RecordId = 1,
                Title = "TEST-TITLE",
            };

            A.CallTo(() => _unitOfWork.BookRepository.GetOne(A<BookFilter>._)).Returns(Task.FromResult<Book?>(null));
            A.CallTo(() => _unitOfWork.AuthorRepository.CheckIfAuthorsExist(createBookDto.AuthorIds)).Returns(existingAuthors);
            A.CallTo(() => _unitOfWork.GenreRepository.CheckIfGenresExist(createBookDto.GenreIds)).Returns(existingGenres);
            A.CallTo(() => _unitOfWork.BookRepository.Create(A<Book>._)).Returns(createdBook);
            A.CallTo(() => _unitOfWork.BookAuthorRepository.AddRange(A<List<BookAuthor>>._));
            A.CallTo(() => _unitOfWork.BookGenreRepository.AddRange(A<List<BookGenre>>._));
            A.CallTo(() => _unitOfWork.Commit());
            A.CallTo(() => _unitOfWork.BookRepository.GetById(createdBook.RecordId)).Returns(Task.FromResult<Book?>(null));

            //Act
            var result = await _bookService.CreateBook(createBookDto);

            //Assert
            result.IsSuccessful.Should().BeFalse();
            result.ErrorType.Should().Be(expectedErrorType);
            result.Data.Should().BeNull();

            A.CallTo(() => _unitOfWork.Commit()).MustHaveHappened();
            A.CallTo(() => _unitOfWork.BookRepository.Create(A<Book>._)).MustHaveHappened();
        }

        [Fact]
        public async Task BookService_CreateBook_ReturnsSuccess()
        {
            //Arrange
            var createBookDto = new CreateBookDto
            {
                Title = "TEST-TITLE",
                AuthorIds = [1],
                GenreIds = [1]
            };

            var fakeExistingBook = A.Fake<Book>();
            var existingAuthors = GetValidAuthors();
            var existingGenres = GetValidGenres();

            var createdBook = new Book
            {
                RecordId = 1,
                Title = "TEST-TITLE",
            };

            A.CallTo(() => _unitOfWork.BookRepository.GetOne(A<BookFilter>._)).Returns(Task.FromResult<Book?>(null));
            A.CallTo(() => _unitOfWork.AuthorRepository.CheckIfAuthorsExist(createBookDto.AuthorIds)).Returns(existingAuthors);
            A.CallTo(() => _unitOfWork.GenreRepository.CheckIfGenresExist(createBookDto.GenreIds)).Returns(existingGenres);
            A.CallTo(() => _unitOfWork.BookRepository.Create(A<Book>._)).Returns(createdBook);
            A.CallTo(() => _unitOfWork.BookAuthorRepository.AddRange(A<List<BookAuthor>>._));
            A.CallTo(() => _unitOfWork.BookGenreRepository.AddRange(A<List<BookGenre>>._));
            A.CallTo(() => _unitOfWork.Commit());
            A.CallTo(() => _unitOfWork.BookRepository.GetById(createdBook.RecordId)).Returns(createdBook);

            //Act
            var result = await _bookService.CreateBook(createBookDto);

            //Assert
            result.IsSuccessful.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.ErrorType.Should().BeNull();
            result.Data.Should().BeAssignableTo<BookDto?>();

            A.CallTo(() => _unitOfWork.Commit()).MustHaveHappened();
            A.CallTo(() => _unitOfWork.BookRepository.Create(A<Book>._)).MustHaveHappened();
        }


        #region PRIVATE METHODS

        private List<Author> GetValidAuthors() => new()
        {
            new Author { RecordId = 1, FirstName = "TESTNAME", LastName = "TESTLASTNAME" }
        };

        private List<Genre> GetValidGenres() => new()
        {
            new Genre { RecordId = 1, Name = "TEST-GENRE" }
        };

        #endregion
    }
}
