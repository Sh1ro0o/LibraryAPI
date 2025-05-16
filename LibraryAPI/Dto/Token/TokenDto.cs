namespace LibraryAPI.Dto.Token
{
    public class TokenDto
    {
        public required string Token { get; set; }
        public required DateTime ExpiresOn { get; set; }
    }
}
