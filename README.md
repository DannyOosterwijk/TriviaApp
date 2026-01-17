# TriviaApp



**Requirements:**

- visual studio with the "ASP.net and web development" workload
- dotnet 8



**Setup:**

Navigate to the solution in the main folder. Once you open it you should see 2 projects linked to the solution. When you press start, 2 web pages should open, one "Page not found" for the backend API and one with the trivia app. if you see only one window appear, right click on the solution in **solution explorer**, go to **properties**, and make sure that **configure startup projects**, is set to open both projects. 



**Trivia app description:**



To start the trivia app, you select a difficulty on the index page. This will generate 10 questions with the chosen difficulty. every time you select a answer to the question, the app will tell you if you where correct and show you your current score, before moving on to the next question.

After completing all 10 questions, the app will show you your final result and bring you back to the index page.



**Project layout:**



The **TriviaHandlerAPI** contains the backend API. In the **program.cs** file, you can find the most of the logic and the endpoints. all additional logic is placed in the **Models** folder.

The **TriviaWebApp** project contains the front-end logic for the web app. Below is a list of folders for the files I created:
- **Views/Trivia:** The different CSHTML files
- **wwwroot/css:** The css file.
- **Models:** The classes for storing data
- **Data:** The interface class that communicates with the API
- **Controllers:** the class that handles which page to open and send information to the view

