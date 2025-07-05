using LibraryAPI.Common;
using LibraryAPI.Filters;
using LibraryAPI.Interface.Service;
using LibraryAPI.Model;
using LibraryAPI.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace LibraryAPI.Service
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public RefreshTokenService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<string?> GetUserIdByRefreshTokenAsync(string token)
        {
            var refreshToken = await _unitOfWork.RefreshTokenRepository.GetById(token);

            return refreshToken?.UserId;
        }

        public async Task<RefreshToken?> ValidateRefreshTokenAsync(string token)
        {
            var filter = new RefreshTokenFilter
            {
                RecordId = token,
                IsActive = true
            };

            var existingRefreshToken = await _unitOfWork.RefreshTokenRepository.GetOne(filter);

            return existingRefreshToken;
        }

        public async Task<OperationResult<string?>> CreateAndStoreRefreshTokenAsync(string userId)
        {
            //check if user exists
            var existingUser = await _userManager.FindByIdAsync(userId);
            if (existingUser == null)
            {
                return OperationResult<string?>.NotFound(message: "User not found!");
            }

            //Create Refresh Token
            const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string refreshTokenString = new string(Enumerable.Range(0, 32)
                .Select(_ => allowedChars[RandomNumberGenerator.GetInt32(allowedChars.Length)])
                .ToArray());

            var newRefreshToken = new RefreshToken
            {
                RecordId = refreshTokenString,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(1),
            };

            await _unitOfWork.RefreshTokenRepository.Create(newRefreshToken);
            await _unitOfWork.Commit();

            return OperationResult<string?>.Success(newRefreshToken.RecordId);
        }

        public Task<bool> RevokeRefreshTokenAsync(string token)
        {
            throw new NotImplementedException();
        }
    }
}
