using LibraryAPI.Interface.Service;
using System.Security.Claims;

namespace LibraryAPI.Service
{
    public class CurrentUserContext : ICurrentUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserContext(IHttpContextAccessor httpContextAccessor) 
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public IReadOnlyList<string> Roles =>
            _httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role)
                .Select(r => r.Value)
                .ToList() ?? new List<string>();
    }
}
