namespace TriviaWebApp.Models
{
    public class APIQuestion
    {
        public string? type { get; set; }
        public string? difficulty { get; set; }
        public string? category { get; set; }
        public string? question { get; set; }
        public string[]? answers { get; set; }

    }

    public class APIResponse
    {
        public string? QuestionResult { get; set; }
        public int QuestionAmount { get; set; }
        public int CurrentQuestion { get; set; }
        public int QuestionsCorrect { get; set; }
    }
}
