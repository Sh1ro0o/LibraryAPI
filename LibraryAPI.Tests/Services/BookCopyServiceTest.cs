using FakeItEasy;
using FluentAssertions;
using LibraryAPI.Common;
using LibraryAPI.Dto.BookCopy;
using LibraryAPI.Filters;
using LibraryAPI.Interface.Service;
using LibraryAPI.Interface.Utility;
using LibraryAPI.Model;
using LibraryAPI.Service;
using LibraryAPI.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.Tests.Services
{
    public class BookCopyServiceTest
    {
        private readonly IBookCopyService _bookCopyService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISerialNumberGeneratorService _serialNumberGeneratorService;
        public BookCopyServiceTest()
        {
            //Dependencies
            _unitOfWork = A.Fake<IUnitOfWork>();
            _serialNumberGeneratorService = A.Fake<ISerialNumberGeneratorService>();

            //SUT
            _bookCopyService = new BookCopyService(_unitOfWork, _serialNumberGeneratorService);
        }

        [Fact]
        public async Task BookCopyService_GetAll_ReturnsOperationResultSuccessWithMappedDtos()
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

            var bookCopyReturnDto = new List<BookCopyDto>
            {
                new BookCopyDto
                {
                    RecordId = 1,
                    SerialNumber = "TEST-SERIAL",
                    IsAvailable = true,
                    CreateDate = new DateTime(new DateOnly(2005, 12, 12), new TimeOnly(12, 45, 12)),
                    ModifiedDate = null,
                    BookId = 1,
                }
            };

            var expectedResult = new OperationResult<IEnumerable<BookCopyDto>>
            {
                IsSuccessful = true,
                Data = bookCopyReturnDto,
                Message = null,
                ErrorType = null
            };

            A.CallTo(() => _unitOfWork.BookCopyRepository.GetAll(filter)).Returns(bookCopyReturn);

            //Act
            var result = await _bookCopyService.GetAll(filter);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task BookCopyService_CreateBookCopy_ReturnsOperationResultSuccessWithMappedDto()
        {
            //Arrange
            var createBookCopyDto = new CreateBookCopyDto { BookId = 1 };
            var serialNumberTest = "TEST-123";

            A.CallTo(() => _serialNumberGeneratorService.GenereateBookCopySerialNumber()).Returns(serialNumberTest);
            A.CallTo(() => _unitOfWork.BookCopyRepository.Create(A<BookCopy>._)).Returns(A<BookCopy>._);
            A.CallTo(() => _unitOfWork.Commit()).Returns(0);

            var expectedBookCopyReturnDto = new BookCopyDto
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
                Data = expectedBookCopyReturnDto,
                Message = null,
                ErrorType = null
            };

            //Act
            var result = await _bookCopyService.CreateBookCopy(createBookCopyDto);

            //Assert
            result.Data!.CreateDate.Should().BeCloseTo(DateTime.UtcNow, precision: TimeSpan.FromSeconds(1));
            result.Should().BeEquivalentTo(expectedResult, options => options
                .Excluding(x => x.Data!.CreateDate)
            );

            A.CallTo(() => _unitOfWork.BookCopyRepository.Create(A<BookCopy>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.Commit()).MustHaveHappenedOnceExactly();
        }
    }
}
