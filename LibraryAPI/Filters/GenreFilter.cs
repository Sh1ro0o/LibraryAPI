namespace LibraryAPI.Filters
{
    public class GenreFilter
    {
        public int? RecordId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
