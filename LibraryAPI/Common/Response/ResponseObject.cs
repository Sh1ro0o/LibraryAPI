namespace LibraryAPI.Common.Response
{
    public class ResponseObject<T>
    {
        public T? Data { get; set; } //Data being returned from the API
        public string? Message { get; set; } //Optional message (for error or success)
    }
}
