namespace TriviaWebApp.Models
{
    public class TriviaResponse
    {
        public string? QuestionResult { get; set; }
        public int QuestionAmount { get; set; }
        public int CurrentQuestion { get; set; }
        public int QuestionsCorrect { get; set; }
    }
}
