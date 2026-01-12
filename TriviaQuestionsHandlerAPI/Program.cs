using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TriviaQuestionsHandlerAPI;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

string url = URLGenerator.GenerateURL();
HttpClient client = new HttpClient();
string response = await client.GetStringAsync(url);
var data = JsonConvert.DeserializeObject<OpenTriviaResponse>(response);
TriviaQuestion CurrentQuestion;
int CurrentQuestionIndex = 0;
int correctQuestions = 0;


app.MapPost("/ResetTrivia", async (CheckAnswerRequest req) =>
{
    URLGenerator.Difficulty difficulty;
    switch (req.Answer)
    {
        case "Easy":
            difficulty = URLGenerator.Difficulty.easy; 
            break;
        case "Medium":
            difficulty = URLGenerator.Difficulty.medium;
            break;
        case "Hard":
            difficulty = URLGenerator.Difficulty.hard;
            break;
        default:
            difficulty = URLGenerator.Difficulty.any;
            break;
    }

    response = await client.GetStringAsync(URLGenerator.GenerateURL(difficulty:difficulty));
    data = JsonConvert.DeserializeObject<OpenTriviaResponse>(response);
    CurrentQuestionIndex = 0;
    correctQuestions = 0;
    return Results.Ok(data.response_code);
});

app.MapGet("/GetQuestion", () =>
{
    if (data != null){
        if (CurrentQuestionIndex >= data.results.Length)
        {
            return JsonConvert.SerializeObject(new Question());
        }
        CurrentQuestion = data.results[CurrentQuestionIndex];
        Question question = new Question(CurrentQuestion);

        return JsonConvert.SerializeObject(question);
    }
    return "Error: Data Not Valid";
});


app.MapPost("/CheckAnswer", (CheckAnswerRequest req) =>
{
    APIResponse response = new APIResponse();
    if(CurrentQuestionIndex < data.results.Length)
    {
        if (req.Answer.Equals(data.results[CurrentQuestionIndex].correct_answer, StringComparison.OrdinalIgnoreCase))
        {
            response.QuestionResult = "Correct";
            correctQuestions++;
        }
        else
        {
            response.QuestionResult = "Incorrect";
        }
    }
    else
    {
        response.QuestionResult = "TriviaFinished";
    }

    response.QuestionsCorrect = correctQuestions;
    response.QuestionAmount = data.results.Length;
    response.CurrentQuestion = CurrentQuestionIndex + 1;

    CurrentQuestionIndex++;
    return JsonConvert.SerializeObject(response);
});

app.Run();