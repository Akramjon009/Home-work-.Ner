using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home_work
{
    public static class Methods
    {
       
        public static void WorkerList()
        {
            var joindata = Info.GetWorker().Join(Info.LanguageList(), wrk => wrk.language_id,
                ln => ln.id, (WorkerList, LanguageList) => new { WorkerList.name, LanguageList.LanguageName }).Where(n => n.LanguageName == "C#");
            foreach (var worker in joindata)
            {
                Console.WriteLine(worker.name + " -> " + worker.LanguageName);
            }
        }
    }
}
