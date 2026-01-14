namespace TriviaQuestionsHandlerAPI
{
    public class OpenTriviaAPIQuestion
    {
        public string? type {  get; set; }
        public string? difficulty { get; set; }
        public string? category { get; set; }
        public string? question { get; set; }
        public string? correct_answer { get; set; }
        public string[]? incorrect_answers { get; set; }

    }

    public class TriviaQuestion
    {
        public TriviaQuestion(OpenTriviaAPIQuestion? trivia = null)
        {
            if(trivia != null)
            {
                this.type = trivia.type;
                this.difficulty = trivia.difficulty;
                this.category = trivia.category;
                this.question = trivia.question;

                //Get both correct and incorrect answers from the trivia question and randomize them
                //Make an array for all answers
                int amountOfAnswers = trivia.incorrect_answers.Length + 1;
                answers = new string[amountOfAnswers];
                
                //Randomly determine the index of the correct answer
                var rng = new Random();
                int correctAnswerIndex = rng.Next(0, amountOfAnswers);

                //Add correct and incorrect answers to the array
                int y = 0;
                for (int i = 0; i < answers.Length; i++)
                {
                    if (i == correctAnswerIndex)
                    {
                        answers[i] = trivia.correct_answer;
                        continue;
                    }

                    answers[i] = trivia.incorrect_answers[y];
                    y++;
                }
            }
        }

        public string? type { get; set; }
        public string? difficulty { get; set; }
        public string? category { get; set; }
        public string? question { get; set; }
        public string[]? answers { get; set; }

    }

    public class APIResponse
    {
        public string? QuestionResult {  get; set; }
        public int QuestionAmount { get; set; }
        public int CurrentQuestion { get; set; }
        public int QuestionsCorrect { get; set; }
    }
}
