using FakeItEasy;
using LibraryAPI.Interface.Service;
using LibraryAPI.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LibraryAPI.Tests.Services
{
    public class TokenServiceTests
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public TokenServiceTests()
        {
            _configuration = A.Fake<IConfiguration>();

            _tokenService = new TokenService(_configuration);
        }
    }
}
