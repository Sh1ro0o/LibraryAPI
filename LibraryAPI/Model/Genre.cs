using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Model
{
    public class Genre
    {
        [Key]
        public int RecordId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    }
}
