namespace LibraryAPI.Interface.Service
{
    public interface IAssistantChatService
    {
        Task<string> GetAssistantResponseAsync(string message);
    }
}
