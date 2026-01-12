using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http.Json;
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

        public async Task ResetTrivia(string difficulty)
        {
            HttpResponseMessage getData = await _httpClient.PostAsJsonAsync("ResetTrivia", new { Answer = difficulty});

            if (getData.IsSuccessStatusCode) 
            {
                string result = getData.Content.ReadAsStringAsync().Result;
            }
            else
            {
                throw new Exception("Could not reset Trivia");
            }
        }

        public async Task<Question> GetQuestion()
        {
            HttpResponseMessage getData = await _httpClient.GetAsync("GetQuestion");

            if (getData.IsSuccessStatusCode)
            {
                string result = getData.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Question>(result);
            }
            else
            {
                throw new Exception("Could not get question from API");
            }
        }

        public async Task<APIResponse> CheckAnswer(string answer)
        {
            string responseString = "";
            HttpResponseMessage getData = await _httpClient.PostAsJsonAsync("CheckAnswer", new { Answer = answer });
            if (getData.IsSuccessStatusCode)
            {
                try
                {
                    responseString = getData.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<APIResponse>(responseString);
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
