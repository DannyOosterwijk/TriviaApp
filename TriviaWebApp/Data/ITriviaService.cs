using TriviaWebApp.Models;

namespace TriviaWebApp.Data
{
    public interface ITriviaService
    {
        public Task<APIQuestion> GetQuestion();
        public Task<APIResponse> CheckAnswer(string answer);
        public Task ResetTrivia(string difficulty);
    }
}
