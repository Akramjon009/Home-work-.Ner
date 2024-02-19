namespace LINQLesson
{
    public class Info
    {
        public List<Univorsaty> GetAll()
        {
            List<Univorsaty> list = new List<Univorsaty>()
            {
                new Univorsaty { Id = 1, Name = "Najot Ta'lim", Location = "Chilonzor", Students  =
                new List<Student>() {
                    new Student() { Id = 1, Age = 27, studyType = "Budjet", FirstName = "Akramjon", LastName = "Abduvahobov" },
                    new Student() { Id = 2, Age = 20, studyType = "Cantraqt", FirstName = "Abduxoliq", LastName = "Abduxoliqov" },
                    new Student() { Id = 3, Age = 23, studyType = "Budjet", FirstName = "Muhammad Abdulloh", LastName = "Muhammad Abdullohov" },
                    new Student() { Id = 4, Age = 40, studyType = "Cantraqt", FirstName = "Ikromjon", LastName = "Ikromjon" },
                } },
                new Univorsaty { Id = 2, Name = "Mohir Dev", Location = "Mirzo Ulug'bek", Students  =
                new List<Student>() {
                    new Student() { Id = 1, Age = 30, studyType = "Cantraqt", FirstName = "Akramjon Mohirdev", LastName = "Abduvahobov Mohirdev" },
                    new Student() { Id = 2, Age = 17, studyType = "Budjet", FirstName = "Abduxoliq Mohirdev", LastName = "Abduxoliqov Mohirdev" },
                    new Student() { Id = 3, Age = 20, studyType = "Cantraqt", FirstName = "Muhammad Abdulloh Mohirdev", LastName = "Muhammad Abdullohov Mohirdev" },
                    new Student() { Id = 4, Age = 31, studyType = "Cantraqt", FirstName = "Ikromjon Mohirdev", LastName = "Ikromjon Mohirdev" },
                } },

            };

            return list;

        }
        public IEnumerable<Student> GetCenterByNameWithExperience()
        {
            var result = GetAll().Where(y=> y.Name == "Najot Ta'lim").SelectMany(x => x.Students).Where(z => z.studyType == "Cantraqt");

            return result;
        }
    }
}
