namespace LibraryAPI.Dto.Genre
{
    public class GenreDto
    {
        public int RecordId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
