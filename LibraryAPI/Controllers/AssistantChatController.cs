﻿using LibraryAPI.Common.Response;
using LibraryAPI.Dto.AssistantChat;
using LibraryAPI.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("AssistantChat")]
    [ApiController]
    public class AssistantChatController : ControllerBase
    {
        private readonly IAssistantChatService _assistantChatService;
        public AssistantChatController(IAssistantChatService assistantChatService)
        {
            _assistantChatService = assistantChatService;
        }

        [Authorize]
        [ProducesResponseType(200, Type = typeof(ResponseObject<string>))] //OK
        [ProducesResponseType(400)] //Bad Request
        [ProducesResponseType(500)] //Internal Server Error
        [HttpPost("assistantResponse")]
        public async Task<IActionResult> GetAssistantResponse([FromBody] AssistantMessageRequest messageRequest)
        {
            var result = await _assistantChatService.GetAssistantResponseAsync(messageRequest);

            return result.ToActionResult();
        }
    }
}
