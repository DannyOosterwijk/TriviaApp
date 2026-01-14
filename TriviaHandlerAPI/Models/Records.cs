namespace TriviaQuestionsHandlerAPI.Models
{
    public record CheckAnswerRequest(string Answer);
    public record ResetTriviaRequest(string Difficulty);
}
