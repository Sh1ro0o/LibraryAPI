namespace LibraryAPI.Filters
{
    public class RefreshTokenFilter
    {
        public int? RecordId { get; set; }
        public string? UserId { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? RevokedAt { get; set; }
        public bool? IsActive { get; set; }

        public bool IncludeUser { get; set; } = true; //Include Users
    }
}
