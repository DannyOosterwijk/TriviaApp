using Newtonsoft.Json;
using TriviaWebApp.Models;

namespace TriviaWebApp.Data
{
    public class TriviaService : ITriviaService
    {
        
        private readonly HttpClient _httpClient;
        public TriviaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //makes a request to the trivia api to reset the questions with the desired difficulty
        //(Options: Easy, Medium, Hard, Any)
        public async Task ResetTrivia(string difficulty)
        {
            HttpResponseMessage getData = await _httpClient.PostAsJsonAsync("ResetTrivia", new { Difficulty = difficulty});

            if (getData.IsSuccessStatusCode) 
            {
                string result = getData.Content.ReadAsStringAsync().Result;
            }
            else
            {
                throw new Exception("Could not start Trivia, Try again.");
            }
        }

        //Makes a request to the API to get the current trivia question
        public async Task<TriviaQuestion> GetQuestion()
        {
            HttpResponseMessage getData = await _httpClient.GetAsync("GetQuestion");

            if (getData.IsSuccessStatusCode)
            {
                string result = getData.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<TriviaQuestion>(result);
            }
            else
            {
                throw new Exception("Could not get question from API");
            }
        }

        //makes a request to the API to check if the provided answer is correct 
        //and returns a response with the result and the current score for the player
        public async Task<TriviaResponse> CheckAnswer(string answer)
        {
            string responseString = "";
            HttpResponseMessage getData = await _httpClient.PostAsJsonAsync("CheckAnswer", new { Answer = answer });
            if (getData.IsSuccessStatusCode)
            {
                try
                {
                    responseString = getData.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<TriviaResponse>(responseString);
                }
                catch (Exception ex) 
                {
                    throw new Exception("CheckAnswer could not convert API response from json to class. Json: " + responseString);
                }
            }
            else
            {
                throw new Exception("CheckAnswer failed to get answer from API, Succescode: " + getData.StatusCode);
            }
        }
    }
}
