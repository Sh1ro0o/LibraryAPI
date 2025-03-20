namespace LibraryAPI.Filters
{
    public class AuthorFilter
    {
        public int? RecordId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? ExcludeRecordId { get; set; }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
