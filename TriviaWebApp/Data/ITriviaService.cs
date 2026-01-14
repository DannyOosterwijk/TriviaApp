using TriviaWebApp.Models;

namespace TriviaWebApp.Data
{
    public interface ITriviaService
    {
        public Task<TriviaQuestion> GetQuestion();
        public Task<TriviaResponse> CheckAnswer(string answer);
        public Task ResetTrivia(string difficulty);
    }
}
