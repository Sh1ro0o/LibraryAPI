using FakeItEasy;
using FluentAssertions;
using LibraryAPI.Interface.Utility;
using LibraryAPI.Service;
using System.Text.RegularExpressions;

namespace LibraryAPI.Tests.Services
{
    public class SerialNumberGeneratorServiceTests
    {
        private readonly ISerialNumberGeneratorService _serialNumberGeneratorService;
        public SerialNumberGeneratorServiceTests()
        {
            _serialNumberGeneratorService = new SerialNumberGeneratorService();
        }

        [Fact]
        public void SerialNumberGeneratorService_GenereateBookCopySerialNumber_ReturnSerialNumber()
        {
            //Arrange
            var regex = new Regex("^SN-\\d{8}-[A-Z0-9]{6}$");

            //Act
            var result = _serialNumberGeneratorService.GenerateBookCopySerialNumber();

            //Assert
            result.Should().NotBeNull();
            result.Should().MatchRegex(regex);
        }
    }
}
