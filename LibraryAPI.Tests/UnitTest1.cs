using FluentAssertions;
using LibraryAPI.Common.Constants;

namespace LibraryAPI.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //Arrange
            string k = "User"; 
            //Act
            var result = k;

            //Assert
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Be(Roles.User);
        }
    }
}