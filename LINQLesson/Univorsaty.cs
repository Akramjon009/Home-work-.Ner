using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQLesson
{
    public class Univorsaty
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Location { get; set; }

        public List<Student> Students { get; set; }
    }
}
