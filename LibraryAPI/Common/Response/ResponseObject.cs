namespace LibraryAPI.Common.Response
{
    public class ResponseObject
    {
        public string? Message { get; set; }
    }

    public class ResponseObject<T>
    {
        public T? Data { get; set; } //Data being returned from the API
        public string? Message { get; set; } //Optional message (for error or success)
        public int? TotalCount { get; set; } //Pagination - total available rows
    }
}
