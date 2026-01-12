using TriviaWebApp.Models;

namespace TriviaWebApp.Data
{
    public interface ITriviaService
    {
        public Task<Question> GetQuestion();
        public Task<APIResponse> CheckAnswer(string answer);
        public Task ResetTrivia(string difficulty);
    }
}
