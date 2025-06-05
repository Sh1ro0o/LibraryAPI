namespace LibraryAPI.Common.Response
{
    public class AiResponse
    {
        public string Intent { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
    }
}
