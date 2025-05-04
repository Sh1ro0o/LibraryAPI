using FakeItEasy;
using FluentAssertions;
using LibraryAPI.Common;
using LibraryAPI.Common.Enums;
using LibraryAPI.Dto.BookCopy;
using LibraryAPI.Filters;
using LibraryAPI.Interface.Service;
using LibraryAPI.Interface.Utility;
using LibraryAPI.Model;
using LibraryAPI.Service;
using LibraryAPI.UnitOfWork;

namespace LibraryAPI.Tests.Services
{
    public class BookCopyServiceTests
    {
        private readonly IBookCopyService _bookCopyService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISerialNumberGeneratorService _serialNumberGeneratorService;
        public BookCopyServiceTests()
        {
            //Dependencies
            _unitOfWork = A.Fake<IUnitOfWork>();
            _serialNumberGeneratorService = A.Fake<ISerialNumberGeneratorService>();

            //SUT
            _bookCopyService = new BookCopyService(_unitOfWork, _serialNumberGeneratorService);
        }

        [Fact]
        public async Task BookCopyService_GetAll_ReturnsSuccess()
        {
            //Arrange
            BookCopyFilter filter = new BookCopyFilter();
            var bookCopyReturn = new List<BookCopy>
            {
                new BookCopy
                {
                    RecordId = 1,
                    SerialNumber = "TEST-SERIAL",
                    IsAvailable = true,
                    CreateDate = new DateTime(new DateOnly(2005, 12, 12), new TimeOnly(12, 45, 12)),
                    ModifiedDate = null,
                    BookId = 1,
                    Book = null
                }
            };

            A.CallTo(() => _unitOfWork.BookCopyRepository.GetAll(filter)).Returns(bookCopyReturn);

            //Act
            var result = await _bookCopyService.GetAll(filter);

            //Assert
            result.IsSuccessful.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Should().BeAssignableTo<IEnumerable<BookCopyDto>>();
        }

        [Fact]
        public async Task BookCopyService_CreateBookCopy_ReturnsSuccessWithMappedDto()
        {
            //Arrange
            var createBookCopyDto = new CreateBookCopyDto { BookId = 1 };
            var serialNumberTest = "TEST-123";

            A.CallTo(() => _serialNumberGeneratorService.GenerateBookCopySerialNumber()).Returns(serialNumberTest);
            A.CallTo(() => _unitOfWork.BookCopyRepository.Create(A<BookCopy>._));
            A.CallTo(() => _unitOfWork.Commit());

            var expectedBookCopyDtoReturn = new BookCopyDto
            {
                RecordId = 0,
                BookId = 1,
                SerialNumber = serialNumberTest,
                IsAvailable = true,
                CreateDate = DateTime.UtcNow,
                ModifiedDate = null
            };

            var expectedResult = new OperationResult<BookCopyDto>
            {
                IsSuccessful = true,
                Data = expectedBookCopyDtoReturn,
                Message = null,
                ErrorType = null
            };

            //Act
            var result = await _bookCopyService.CreateBookCopy(createBookCopyDto);

            //Assert
            result.Data.Should().NotBeNull();
            result.Data!.CreateDate.Should().BeCloseTo(DateTime.UtcNow, precision: TimeSpan.FromSeconds(1));
            result.Should().BeEquivalentTo(expectedResult, options => options
                .Excluding(x => x.Data!.CreateDate)
            );
            A.CallTo(() => _unitOfWork.BookCopyRepository.Create(A<BookCopy>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.Commit()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task BookCopyService_UpdateBookCopy_ReturnsBookCopyNotFound()
        {
            //Arrange
            var expectedErrorType = OperationErrorType.NotFound;

            var saveBookCopyDto = new SaveBookCopyDto
            {
                RecordId = 1,
                IsAvailable = true,
                BookId = 1
            };

            A.CallTo(() => _unitOfWork.BookCopyRepository.GetById(saveBookCopyDto.RecordId)).Returns(Task.FromResult<BookCopy?>(null));

            //Act
            var result = await _bookCopyService.UpdateBookCopy(saveBookCopyDto);

            //Assert
            result.IsSuccessful.Should().BeFalse();
            result.ErrorType.Should().Be(expectedErrorType);
        }

        [Fact]
        public async Task BookCopyService_UpdateBookCopy_ReturnsBookNotFound()
        {
            //Arrange
            var expectedErrorType = OperationErrorType.NotFound;
            var bookCopyFake = A.Fake<BookCopy>();

            var saveBookCopyDto = new SaveBookCopyDto
            {
                RecordId = 1,
                IsAvailable = true,
                BookId = 1,
            };


            A.CallTo(() => _unitOfWork.BookCopyRepository.GetById(saveBookCopyDto.RecordId)).Returns(bookCopyFake);
            A.CallTo(() => _unitOfWork.BookRepository.GetById(saveBookCopyDto.BookId.Value)).Returns(Task.FromResult<Book?>(null));

            //Act
            var result = await _bookCopyService.UpdateBookCopy(saveBookCopyDto);

            //Assert
            result.IsSuccessful.Should().BeFalse();
            result.ErrorType.Should().Be(expectedErrorType);
        }

        [Fact]
        public async Task BookCopyService_UpdateBookCopy_ReturnsSuccessWithMappedDto()
        {
            //Arrange
            var serialNumberTest = "TEST-123";
            var fakeBook = A.Fake<Book>();

            var saveBookCopyDto = new SaveBookCopyDto
            {
                RecordId = 1,
                IsAvailable = true,
                BookId = 1,
            };

            var bookCopy = new BookCopy
            {
                RecordId = 1,
                BookId = 99,
                SerialNumber = serialNumberTest,
                IsAvailable = false,
                CreateDate = DateTime.UtcNow,
                ModifiedDate = null
            };

            var expectedBookCopyDtoReturn = new BookCopyDto
            {
                RecordId = 1,
                BookId = 1,
                SerialNumber = serialNumberTest,
                IsAvailable = true,
                CreateDate = bookCopy.CreateDate,
                ModifiedDate = DateTime.UtcNow,
            };

            var expectedResult = new OperationResult<BookCopyDto>
            {
                IsSuccessful = true,
                Data = expectedBookCopyDtoReturn,
                Message = null,
                ErrorType = null
            };

            A.CallTo(() => _unitOfWork.BookCopyRepository.GetById(saveBookCopyDto.RecordId)).Returns(bookCopy);
            A.CallTo(() => _unitOfWork.BookRepository.GetById(saveBookCopyDto.BookId.Value)).Returns(fakeBook);
            A.CallTo(() => _unitOfWork.BookCopyRepository.Update(A<BookCopy>._));
            A.CallTo(() => _unitOfWork.Commit());

            //Act
            var result = await _bookCopyService.UpdateBookCopy(saveBookCopyDto);

            //Assert
            result.IsSuccessful.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data?.ModifiedDate.Should().NotBeNull();
            result.Data?.ModifiedDate.Should().BeCloseTo(DateTime.UtcNow, precision: TimeSpan.FromSeconds(1));
            result.Should().BeEquivalentTo(expectedResult, options => options
                .Excluding(x => x.Data.ModifiedDate)
            );
        }

        [Fact]
        public async Task BookCopyService_DeleteBookCopy_ReturnsBookCopyNotFound()
        {
            //Arrange
            int id = 5;
            var expectedErrorType = OperationErrorType.NotFound;

            A.CallTo(() => _unitOfWork.BookCopyRepository.GetById(id)).Returns(Task.FromResult<BookCopy?>(null));

            //Act
            var result = await _bookCopyService.DeleteBookCopy(id);

            //Assert
            result.IsSuccessful.Should().BeFalse();
            result.ErrorType.Should().Be(expectedErrorType);
        }

        [Fact]
        public async Task BookCopyService_DeleteBookCopy_ReturnsSuccess()
        {
            //Arrange
            int id = 5;
            var bookCopyFake = A.Fake<BookCopy>();

            A.CallTo(() => _unitOfWork.BookCopyRepository.GetById(id)).Returns(bookCopyFake);

            //Act
            var result = await _bookCopyService.DeleteBookCopy(id);

            //Assert
            result.IsSuccessful.Should().BeTrue();
            result.Data.Should().BeTrue();
        }
    }
}
