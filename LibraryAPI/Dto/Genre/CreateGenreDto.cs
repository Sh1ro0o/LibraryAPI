using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Dto.Genre
{
    public class CreateGenreDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
