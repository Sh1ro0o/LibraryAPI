namespace LibraryAPI.Filters
{
    public class SeperateTransactionsCountFilter
    {
        public bool HasBorrowedToday { get; set; } = false;
        public bool HasReturnedToday { get; set; } = false;
        public bool HasOverdueToday { get; set; } = false;
        public string? UserId { get; set; }
    }
}
