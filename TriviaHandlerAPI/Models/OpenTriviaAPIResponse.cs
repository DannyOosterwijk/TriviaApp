namespace TriviaQuestionsHandlerAPI.Models
{
    public class OpenTriviaAPIResponse
    {
        public string? response_code { get; set; }
        public OpenTriviaAPIQuestion[]? results { get; set; }
    }
}
