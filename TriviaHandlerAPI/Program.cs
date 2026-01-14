using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TriviaQuestionsHandlerAPI.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Global variables
string url = URLGenerator.GenerateURL();
HttpClient client = new HttpClient();

string response = await client.GetStringAsync(url);
OpenTriviaAPIResponse triviaResponse = JsonConvert.DeserializeObject<OpenTriviaAPIResponse>(response);

OpenTriviaAPIQuestion CurrentQuestion;
int CurrentQuestionIndex = 0;
int correctQuestions = 0;

//Generate a new URl with the chosen difficulty, and get a new list of questions
app.MapPost("/ResetTrivia", async (ResetTriviaRequest req) =>
{
    //Generate a string which includes the parameters for the open trivia api
    response = await client.GetStringAsync(URLGenerator.GenerateURL(difficulty:URLGenerator.StringToDifficulty(req.Difficulty)));
    //get a new list of questions from the open trivia api
    triviaResponse = JsonConvert.DeserializeObject<OpenTriviaAPIResponse>(response);

    //set default values to keep track of which question the player is on
    CurrentQuestionIndex = 0;
    correctQuestions = 0;

    return Results.Ok(triviaResponse.response_code);
});

//Get the current question from the list of questions
app.MapGet("/GetQuestion", () =>
{
    if (triviaResponse != null){
        //If the player goes past the final question, return an empy question
        if (CurrentQuestionIndex >= triviaResponse.results.Length)
        {
            return JsonConvert.SerializeObject(new TriviaQuestion());
        }

        //store the new current question, and randomly combine the correct and incorrect answers 
        //(combined in the Trivia Question constructor)
        CurrentQuestion = triviaResponse.results[CurrentQuestionIndex];
        TriviaQuestion question = new TriviaQuestion(CurrentQuestion);

        return JsonConvert.SerializeObject(question);
    }
    return "Error: Data Not Valid";
});

//Check if the answer submitted is ccorrect for the current question.
//returns if the answer is correct and statistics of the current list of questions
app.MapPost("/CheckAnswer", (CheckAnswerRequest req) =>
{
    TriviaResponse response = new TriviaResponse();
    //check if the current index is inside of the list of questions
    if(CurrentQuestionIndex < triviaResponse.results.Length)
    {
        //compare the chosen answer with the correct answer of the current question
        if (req.Answer.Equals(triviaResponse.results[CurrentQuestionIndex].correct_answer, StringComparison.OrdinalIgnoreCase))
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

    //set statistic of the current list of questions
    response.QuestionsCorrect = correctQuestions;
    response.QuestionAmount = triviaResponse.results.Length;
    response.CurrentQuestion = CurrentQuestionIndex + 1;

    CurrentQuestionIndex++;
    return JsonConvert.SerializeObject(response);
});

app.Run();