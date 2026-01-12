namespace TriviaQuestionsHandlerAPI
{
    public class OpenTriviaResponse
    {
        public string? response_code { get; set; }
        public TriviaQuestion[]? results { get; set; }
    }
}
