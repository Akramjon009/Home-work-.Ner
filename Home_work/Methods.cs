namespace Home_work
{
    public static class Methods
    {
        
        public static void WorkerList()
        {
            var joindata = Info.GetWorker().Join(Info.LanguageList(), wrk => wrk.language_id,
                ln => ln.id, (WorkerList, LanguageList) => new { WorkerList.name, LanguageList.LanguageName });
            foreach (var worker in joindata)
            {
                Console.WriteLine(worker.name + " -> " + worker.LanguageName);
            }
        }
    }
}
