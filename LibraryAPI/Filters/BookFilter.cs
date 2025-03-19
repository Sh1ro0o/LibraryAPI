namespace LibraryAPI.Filters
{
    public class BookFilter
    {
        public int? RecordId { get; set; }
        public string? Title { get; set; }
        public DateOnly? PublishDate { get; set; }
        public string? ISBN { get; set; }
        public int? ExcludeRecordId { get; set; }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
