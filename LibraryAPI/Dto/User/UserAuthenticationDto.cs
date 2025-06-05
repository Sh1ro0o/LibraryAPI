namespace LibraryAPI.Dto.User
{
    public class UserAuthenticationDto
    {
        public required string Email { get; set; }
        public required string Token { get; set; }
        public required DateTime ExpiresOn { get; set; }
        public required IList<string> Roles { get; set; }
    }
}
