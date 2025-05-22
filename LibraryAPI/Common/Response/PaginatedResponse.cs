namespace LibraryAPI.Common.Response
{
    public class PaginatedResponse<T>
    {
        public ICollection<T> Data { get; set; } = new List<T>();
        public int TotalItems { get; set; } = 0;
    }
}
