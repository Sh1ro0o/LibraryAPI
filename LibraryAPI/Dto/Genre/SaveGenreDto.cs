using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Dto.Genre
{
    public class SaveGenreDto
    {
        [Required]
        public required int RecordId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
