namespace LibraryAPI.Filters
{
    public class UserFilter
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
