public class Check
{
    public  class Person
    {
        public string Name { get; set; }

        public Person(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            if (obj is null || GetType() != obj.GetType())
            {
                return false;
            }

            Person otherPerson = (Person)obj;
            return Name == otherPerson.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }

}

