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

namespace LibraryAPI.Tests
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
        public async Task BookCopyService_GetAll_ReturnsOperationResultSuccess()
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

            var expectedResult = OperationResult<IEnumerable<BookCopyDto>>.Success(bookCopyReturnDto);

            A.CallTo(() => _unitOfWork.BookCopyRepository.GetAll(filter)).Returns(bookCopyReturn);

            //Act
            var result = await _bookCopyService.GetAll(filter);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
