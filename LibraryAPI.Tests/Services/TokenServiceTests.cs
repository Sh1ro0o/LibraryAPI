using FakeItEasy;
using LibraryAPI.Interface.Service;
using LibraryAPI.Model;
using LibraryAPI.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LibraryAPI.Tests.Services
{
    public class TokenServiceTests
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;

        public TokenServiceTests()
        {
            _configuration = A.Fake<IConfiguration>();
            _userManager = A.Fake<UserManager<AppUser>>();

            _tokenService = new TokenService(_configuration, _userManager);
        }
    }
}
