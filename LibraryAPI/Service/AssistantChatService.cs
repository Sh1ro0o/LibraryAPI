using LibraryAPI.Interface.Service;
using LibraryAPI.UnitOfWork;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using LibraryAPI.Common.Constants;
using LibraryAPI.Filters;
using LibraryAPI.Common.Response;

namespace LibraryAPI.Service
{
    public class AssistantChatService : IAssistantChatService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _OpenApiKey;

        public AssistantChatService(IUnitOfWork unitOfWork, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient();
            _OpenApiKey = _configuration["OpenAPI:ApiKey"]!;
        }

        public async Task<string> GetAssistantResponseAsync(string message)
        {
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
                    new { role = "user", content = message },
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
                            if (books is null)
                            {
                                reply = $"I was not able to find any books containing a title: { bookTitle }";
                            }
                            else
                            {
                                var titlesOfBooks = books.Data.Select(x => x.Title);
                                string titlesFormatted = string.Join(", ", titlesOfBooks);

                                reply = $"I have found {books.TotalItems} book/s available: {titlesFormatted} ";
                            }
                        }
                        else
                        {
                            throw new Exception("Unexpected response from OpenAI.");
                        }
                        break;

                    //ASK FOR BOOK NAME
                    case OpenAiResponses.ASK_FOR_BOOK_NAME:
                        reply = "Could you please provide a title of the book you are looking for?";
                        break;

                    //GET ALL BOOK GENRES
                    case OpenAiResponses.GET_ALL_BOOK_GENRES:
                        var filter = new GenreFilter();
                        var genres = await _unitOfWork.GenreRepository.GetAll(filter);

                        var genreNames = genres.Data.Select(x => x.Name);
                        string genresFormatted = string.Join(", ", genreNames);

                        reply = $"We currently offer books in the following genres: {genresFormatted}";
                        break;

                    //GET OVERDUE BOOKS
                    case OpenAiResponses.GET_OVERDUE_BOOKS:

                        break;

                    //DEFAULT REPLY
                    default:
                        reply = openAIintent;
                        break;
                }
            }
            else
            {
                throw new Exception("Unexpected response from OpenAI.");
            }

            return reply;
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
