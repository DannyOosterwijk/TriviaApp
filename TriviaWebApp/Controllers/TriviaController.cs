using Microsoft.AspNetCore.Mvc;
using TriviaWebApp.Data;
using TriviaWebApp.Models;

namespace TriviaWebApp.Controllers
{
    public class TriviaController : Controller
    {
        //Create and bind the trivia interface
        private ITriviaService _triviaService;
        public TriviaController(ITriviaService triviaService)
        {
            _triviaService = triviaService;
        }

        //Main page where the player decided the difficulty they want to play
        public async Task<IActionResult> Index()
        {
            return View();
        }

        //Tries to start the Trivia by sending the desired difficulty to the Trivia API
        public async Task<IActionResult> StartTrivia(string difficulty)
        {
            try
            {
                await _triviaService.ResetTrivia(difficulty);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", new { error = ex.Message });
            }
            return RedirectToAction("Trivia");
        }

        //Trivia page where the trivia question is shown and the player selects an answer
        public async Task<IActionResult> Trivia()
        {
            APIQuestion currentQuestion;

            try
            {
                currentQuestion = await _triviaService.GetQuestion();
                if (currentQuestion.question == null)
                {
                    RedirectToAction("ErrorPage", new { error = "LastQuestionReached" });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", new { error = ex.Message });
            }
            return View(currentQuestion);
        }

        //Check if the players answer is correct and move to Show answer page
        public async Task<IActionResult> CheckAnswer (string answer)
        {
            try
            {
                var result = await _triviaService.CheckAnswer(answer);
                return RedirectToAction("ShowAnswer", result);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", new { error = ex.Message });
            }
        }

        //Show if the player's answer was correct and hoow manny questions they have wrong and correct
        public IActionResult ShowAnswer(APIResponse answer)
        {
            return View(answer);
        }

        //Page for handling and displaying errors
        public IActionResult ErrorPage(string error)
        {
            return View((object)error);
        }

        //Final page of the trivia. shows the final score the player has recieved
        public async Task<IActionResult> TriviaFinished()
        {
            try
            {
                var result = await _triviaService.CheckAnswer("");
                return View(result);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", new { error = ex.Message });
            }
        }
    }
}
