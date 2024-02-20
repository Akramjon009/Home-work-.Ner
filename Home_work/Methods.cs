using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home_work
{
    public static class Methods
    {
        public static List<Worker> GetWorker()
        {
            List<Worker> workerlist = new List<Worker>()
            {
                new Worker()
                {
                    id = 1,
                    name = "Ozodbek",
                    language_id = 3,
                },
                new Worker()
                {
                    id = 2,
                    name = "Sardor",
                    language_id = 1,
                },
                new Worker()
                {
                    id = 3,
                    name = "Abduxoliq",
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
                    LanguageName = "C#"
                },
                new Language()
                {
                    id= 2,
                    LanguageName = "Python"
                },
                new Language()
                {
                    id= 3,
                    LanguageName = "JavaScript"
                },
                new Language()
                {
                    id= 4,
                    LanguageName = "C"
                },
                new Language()
                {
                    id= 5,
                    LanguageName = "Rust"
                }
            };
            return language;

        }
        public static void WorkerList()
        {
            var joindata = GetWorker().Join(LanguageList(), wrk => wrk.language_id,
                ln => ln.id, (WorkerList, LanguageList) => new { WorkerList.name, LanguageList.LanguageName });
            foreach (var worker in joindata)
            {
                Console.WriteLine(worker.name + " -> " + worker.LanguageName);
            }
        }
    }
}
