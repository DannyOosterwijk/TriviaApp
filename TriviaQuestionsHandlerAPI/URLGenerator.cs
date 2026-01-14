namespace TriviaQuestionsHandlerAPI
{
    public static class URLGenerator
    {

        //assemble an URL for the Open trivia API with the chosen parameters
        public static string GenerateURL(Category category = Category.Any, Difficulty difficulty = Difficulty.any, Type type = Type.any, int amount = 10)
        {
            string url = $"https://opentdb.com/api.php?amount={amount}";

            if (category != Category.Any)
            {
                url += $"&category={(int)category}";
            }

            if(difficulty != Difficulty.any)
            {
                url += $"&difficulty={difficulty}";
            }

            if (type != Type.any)
            {
                url += $"&type={type}";
            }

            return url;
        }

        public static Difficulty StringToDifficulty (string difficulty)
        {
            switch (difficulty)
            {
                case "Easy":
                    return URLGenerator.Difficulty.easy;
                case "Medium":
                    return URLGenerator.Difficulty.medium;
                case "Hard":
                    return URLGenerator.Difficulty.hard;
                default:
                    return URLGenerator.Difficulty.any;
            }
        }
        public enum Category
        {
            Any = 0,
            GeneralKnowledge = 9,
            EntertainmentBooks,
            EntertainmentFilm,
            EntertainmentMusic,
            EntertainmentMusicalTheatres,
            EntertainmentTelevision,
            EntertainmentVideoGames,
            EntertainmentBoardGames,
            ScienceNature,
            ScienceComputers,
            ScienceMathemathics,
            Mythology,
            Sports,
            Geography,
            History,
            Politics,
            Art,
            Celebrities,
            Animals,
            Vehicles,
            EntertainmentComics,
            ScienceGadgets,
            EntertainmentAnimeMange,
            EntertainmentCartoonAnimation
        }

        public enum Type
        {
            any,
            multiple,
            boolean
        }

        public enum Difficulty
        {
            any,
            easy,
            medium,
            hard
        }
    }
}
