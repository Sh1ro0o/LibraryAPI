using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Dto.Author
{
    public class SaveAuthorDto
    {
        [Required]
        public required int RecordId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
