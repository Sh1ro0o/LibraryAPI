namespace LibraryAPI.Common.Response
{
    public class AiResponse
    {
        public string Intent { get; set; } = string.Empty;
        public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();
    }
}
