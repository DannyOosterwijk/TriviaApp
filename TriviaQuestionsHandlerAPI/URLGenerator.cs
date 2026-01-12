namespace TriviaQuestionsHandlerAPI
{
    public static class URLGenerator
    {
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
