namespace TriviaQuestionsHandlerAPI
{
    public class OpenTriviaResponse
    {
        public string? response_code { get; set; }
        public OpenTriviaAPIQuestion[]? results { get; set; }
    }
}
