using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Dto.User
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
