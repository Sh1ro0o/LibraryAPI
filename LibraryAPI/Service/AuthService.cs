using LibraryAPI.Common;
using LibraryAPI.Dto.User;
using LibraryAPI.Filters;
using LibraryAPI.Interface.Service;
using LibraryAPI.Model;
using LibraryAPI.UnitOfWork;
using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;

        public AuthService(IRefreshTokenService refreshTokenService, IUnitOfWork unitOfWork, ITokenService tokenService, UserManager<AppUser> userManager)
        {
            _refreshTokenService = refreshTokenService;
            _tokenService = tokenService;
            _userManager = userManager;
        }

        public async Task<OperationResult<UserAuthenticationDto?>> RefreshSessionToken(string refreshToken)
        {
            var refreshTokenData = await _refreshTokenService.ValidateRefreshTokenAsync(refreshToken);
            if (refreshTokenData == null)
            {
                return OperationResult<UserAuthenticationDto?>.NotFound(message: "Token expired");
            }

            var existingUser = await _userManager.FindByIdAsync(refreshTokenData.UserId);
            if (existingUser == null)
            {
                return OperationResult<UserAuthenticationDto?>.NotFound(message: "User Error!");
            }

            var tokenDto = await _tokenService.CreateToken(existingUser);

            var userAuthenticationDto = new UserAuthenticationDto
            {
                Email = existingUser.Email!,
                Token = tokenDto.Token,
                RefreshToken = refreshToken,
                ExpiresOn = tokenDto.ExpiresOn,
                Roles = tokenDto.Roles,
            };

            return OperationResult<UserAuthenticationDto?>.Success(userAuthenticationDto);
        }
    }
}
