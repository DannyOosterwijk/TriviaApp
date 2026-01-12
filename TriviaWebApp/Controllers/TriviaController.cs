using Microsoft.AspNetCore.Mvc;
using TriviaWebApp.Data;
using TriviaWebApp.Models;
using System.Net.Http.Headers;

namespace TriviaWebApp.Controllers
{
    public class TriviaController : Controller
    {
        private Question currentQuestion;
        private ITriviaService _triviaService;
        public TriviaController(ITriviaService triviaService)
        {
            _triviaService = triviaService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

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

        public async Task<IActionResult> Trivia()
        {
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

        public IActionResult ShowAnswer(APIResponse answer)
        {
            return View(answer);
        }

        public IActionResult ErrorPage(string error)
        {
            return View((object)error);
        }

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
