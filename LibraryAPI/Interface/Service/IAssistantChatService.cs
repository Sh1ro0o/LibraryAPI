using LibraryAPI.Common;
using LibraryAPI.Dto.AssistantChat;

namespace LibraryAPI.Interface.Service
{
    public interface IAssistantChatService
    {
        Task<OperationResult<string?>> GetAssistantResponseAsync(AssistantMessageRequest messageRequest);
    }
}
