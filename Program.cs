namespace SI_DevSchedule
{
    public class Lesson
    {
        public string? Title { get; set; }
        public string? Teacher { get; set; }
        public int? Classroom { get; set; }
        public bool isFree { get; set; } = true;

        public Lesson(string title, string teacher, int classroom, bool active = true)
        {
            Title = title;
            Teacher = teacher;
            Classroom = classroom;
            isFree = active;
        }

        public override string ToString()
        {
            return (isFree ? $"{Title} {Classroom}\n{Teacher}\n" : $" Godzina zajęciowa wolna :)");
        }
    }

    public class Schedule
    {
        private static readonly Lesson[] test = { new Lesson("", "", 1), new Lesson("", "", 2) };

        public List<string> ClassName { get; set; } = new List<string>() { "Klasa 1", "Klasa 2", "Klasa 3", "Klasa 4" };

        public Lesson[][] Monday { get; set; } = new Lesson[][] { test, test,test,test };

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Schedule test = new Schedule();

            Console.WriteLine(test.Monday[2][0]);

        }
    }
}
