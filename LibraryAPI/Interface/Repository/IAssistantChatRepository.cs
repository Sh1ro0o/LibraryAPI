using LibraryAPI.Common;
using LibraryAPI.Model;

namespace LibraryAPI.Interface.Repository
{
    public interface IAssistantChatRepository
    {
        Task<ICollection<AssistantChat>> GetChatHistory(string userId);
        Task<AssistantChat> CreateChatLog(AssistantChat model);
    }
}
