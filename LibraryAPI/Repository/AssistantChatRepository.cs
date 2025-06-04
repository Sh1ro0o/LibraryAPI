using LibraryAPI.Data;
using LibraryAPI.Interface.Repository;
using LibraryAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repository
{
    public class AssistantChatRepository : IAssistantChatRepository
    {
        private readonly LibraryDbContext _context;

        public AssistantChatRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<AssistantChat>> GetChatHistory(string userId)
        {
            var chatHistory = await _context.AssistantChat.Where(x => x.UserId == userId).ToListAsync();

            return chatHistory;
        }

        public async Task<AssistantChat> CreateChatLog(AssistantChat model)
        {
            await _context.AssistantChat.AddAsync(model);

            return model;
        }
    }
}
