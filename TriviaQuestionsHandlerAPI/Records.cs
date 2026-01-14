namespace TriviaQuestionsHandlerAPI
{
    public record CheckAnswerRequest(string Answer);
    public record ResetTriviaRequest(string Difficulty);
}
