namespace Home_work
{
    public static class Info
    {
        public static List<Worker> GetWorker()
        {
            List<Worker> workerlist = new List<Worker>()
            {
                new Worker()
                {
                    id = 1,
                    name = "Tohirjon",
                    language_id = 3,
                },
                new Worker()
                {
                    id = 2,
                    name = "Ibrohim",
                    language_id = 1,
                },
                new Worker()
                {
                    id = 3,
                    name = "Ozodbek",
                    language_id = 2,
                },
                new Worker()
                {
                    id = 4,
                    name = "Akramjon",
                    language_id = 4,
                },
            };


            return workerlist;
        }
        public static List<Language> LanguageList()
        {
            List<Language> language = new List<Language>()
            {
                new Language()
                {
                    id= 1,
                    LanguageName = "C++"
                },
                new Language()
                {
                    id= 2,
                    LanguageName = "Python"
                },
                new Language()
                {
                    id= 3,
                    LanguageName = "Java"
                },
                new Language()
                {
                    id= 4,
                    LanguageName = "C#"
                },
                new Language()
                {
                    id= 5,
                    LanguageName = "C"
                }
            };
            return language;

        }
    }
}
