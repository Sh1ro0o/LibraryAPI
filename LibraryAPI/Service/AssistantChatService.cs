﻿using LibraryAPI.Interface.Service;
using LibraryAPI.UnitOfWork;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using LibraryAPI.Common.Constants;
using LibraryAPI.Filters;
using LibraryAPI.Common.Response;
using LibraryAPI.Common;
using LibraryAPI.Dto.AssistantChat;
using LibraryAPI.Model;
using LibraryAPI.Dto.BorrowingTransaction;
using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.Service
{
    public class AssistantChatService : IAssistantChatService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserContext _currentUserContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly string _OpenApiKey;

        public AssistantChatService(IUnitOfWork unitOfWork, IConfiguration configuration, IHttpClientFactory httpClientFactory, ICurrentUserContext currentUserContext, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient();
            _currentUserContext = currentUserContext;
            _userManager = userManager;
            _OpenApiKey = _configuration["OpenAPI:ApiKey"]!;
        }

        public async Task<OperationResult<string?>> GetAssistantResponseAsync(AssistantMessageRequest messageRequest)
        {
            //Check if User exists
            var userIdcontext = _currentUserContext.UserId;
            var existingUser = await _userManager.FindByIdAsync(userIdcontext);
            if (existingUser == null)
            {
                return OperationResult<string?>.NotFound(message: $"User with Id: {userIdcontext} Not Found!");
            }

            //Check if roles assigned
            var roles = _currentUserContext.Roles;
            if (roles.Count < 1)
            {
                return OperationResult<string?>.InternalServerError(message: "Internal server error! Please contact support!");
            }

            string reply = "";
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _OpenApiKey);

            var payload = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new 
                    { 
                        role = "system", 
                        content = OpenAiConstants.Prompt
                    },
                    new { role = "user", content = messageRequest.Message },
                },
                stream = false
            };

            request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            //Response processing
            var responseBody = await response.Content.ReadAsStringAsync();
            var aiResponse = ProcessResponseToAiResponse(responseBody);

            if (aiResponse != null) { 
                var openAIintent = aiResponse.Intent;
                var openAIparameters = aiResponse.Parameters;

                switch(openAIintent)
                {
                    //SEARCH BOOK BY NAME
                    case OpenAiResponses.SEARCH_BOOK_BY_NAME:
                        if (openAIparameters.ContainsKey("bookTitle"))
                        {
                            var bookTitle = openAIparameters.GetValueOrDefault("bookTitle");

                            //get books
                            var bookFilter = new BookFilter
                            {
                                Title = bookTitle
                            };

                            var books = await _unitOfWork.BookRepository.GetAll(bookFilter);
                            if (books.Data.Count > 0)
                            {
                                var titlesOfBooks = books.Data.Select(x => x.Title);
                                string titlesFormatted = string.Join(", ", titlesOfBooks);

                                reply = $"I have found {books.TotalItems} book/s available: {titlesFormatted} ";
                            }
                            else
                            {
                                reply = $"I was not able to find any books containing a title: {bookTitle}";
                            }
                        }
                        else
                        {
                            return OperationResult<string?>.BadGateway(message: "Unexpected response from OpenAI.");
                        }
                        break;

                    //ASK FOR BOOK NAME
                    case OpenAiResponses.ASK_FOR_BOOK_NAME:
                        reply = "Could you please provide a title of the book you are looking for?";
                        break;

                    //GET ALL BOOK GENRES
                    case OpenAiResponses.GET_ALL_BOOK_GENRES:
                        var genreFilter = new GenreFilter();
                        var genres = await _unitOfWork.GenreRepository.GetAll(genreFilter);

                        var genreNames = genres.Data.Select(x => x.Name);
                        string genresFormatted = string.Join(", ", genreNames);

                        reply = $"We currently offer books in the following genres: {genresFormatted}";
                        break;

                    //GET OVERDUE BOOKS
                    case OpenAiResponses.GET_OVERDUE_BOOKS:
                        var overdueFilter = new BorrowingTransactionFilter();
                        overdueFilter.IsReturned = false;
                        overdueFilter.DueDate = DateTime.UtcNow;
                        overdueFilter.IncludeBook = true;

                        if (!roles.Contains(Roles.Admin))
                        {
                            overdueFilter.UserId = userIdcontext;
                        }

                        var overdueBorrows = await _unitOfWork.BorrowingTransactionRepository.GetAll(overdueFilter);

                        if (overdueBorrows.Count > 0)
                        {

                            string overdueBorrowsMessage = "Your overdue books are: " +
                                                string.Join(", ",
                                                    overdueBorrows.Select(b =>
                                                        $"{b.BookCopy?.Book?.Title} (due on {b.DueDate:MMMM dd, yyyy})"
                                                    )
                                                ) + ".";

                            reply = overdueBorrowsMessage;
                        }
                        else
                        {
                            reply = "You currently do not have any overdue books.";
                        }
                        break;

                    //GET BORROWED BOOKS
                    case OpenAiResponses.GET_BORROWED_BOOKS:
                        var borrowedFilter = new BorrowingTransactionFilter();
                        borrowedFilter.IsReturned = false;
                        borrowedFilter.IncludeBook = true;

                        if (!roles.Contains(Roles.Admin))
                        {
                            borrowedFilter.UserId = userIdcontext;
                        }

                        var borrowedBooks = await _unitOfWork.BorrowingTransactionRepository.GetAll(borrowedFilter);

                        if (borrowedBooks.Count > 0)
                        {

                            string borrowedBooksMessage = "Your current borrwed books are: " +
                                                string.Join(", ",
                                                    borrowedBooks.Select(b =>
                                                        $"{b.BookCopy?.Book?.Title} (due on {b.DueDate:MMMM dd, yyyy})"
                                                    )
                                                ) + ".";

                            reply = borrowedBooksMessage;
                        }
                        else
                        {
                            reply = "You currently do not have any borrowed books.";
                        }
                        break;

                    case OpenAiResponses.GREETING:
                        var greetings = new[]
                        {
                            "At your service, hero of the library!",
                            "Greetings, seeker of knowledge!"
                        };
                        var rand = new Random();
                        reply = greetings[rand.Next(greetings.Length)];
                        break;

                    case OpenAiResponses.THANKING:
                        reply = "Hermes has your back. Happy reading from the Mythologix team!";
                        break;

                    case OpenAiResponses.UNKNOWN_INTENT:
                        reply = "Hmm, I’m not sure how to help with that yet. Try asking me about books, genres, or your borrowed items!";
                        break;

                    //DEFAULT REPLY
                    default:
                        reply = "Hmm, I’m not sure how to help with that yet. Try asking me about books, genres, or your borrowed items!";
                        break;
                }
            }
            else
            {
                return OperationResult<string?>.BadGateway(message: "Unexpected response from OpenAI.");
            }

            //Log user message
            var userChat = new AssistantChat
            {
                SenderType = SenderType.User,
                Message = messageRequest.Message,
                UserId = existingUser.Id
            };
            await _unitOfWork.AssistantChatRepository.CreateChatLog(userChat);

            //Log assistant response
            var assistantChat = new AssistantChat
            {
                SenderType = SenderType.Assistant,
                Message = reply,
                UserId = existingUser.Id
            };
            await _unitOfWork.AssistantChatRepository.CreateChatLog(assistantChat);

            await _unitOfWork.Commit();

            return OperationResult<string?>.Success(reply);
        }

        private AiResponse? ProcessResponseToAiResponse(string body)
        {
            var parsedBody = JsonDocument.Parse(body);
            if (parsedBody.RootElement.TryGetProperty("choices", out JsonElement choices) &&
                choices.GetArrayLength() > 0 &&
                choices[0].TryGetProperty("message", out JsonElement resMessage) &&
                resMessage.TryGetProperty("content", out JsonElement content))
            {
                string? rawJson = content.GetString();

                if (!string.IsNullOrWhiteSpace(rawJson))
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    return JsonSerializer.Deserialize<AiResponse>(rawJson, options);
                }
            }

            return null;
        }
    }
}
