namespace SI_DevSchedule
{
    public static class Glob
    {
        public static List<Lesson> DefultLessons = new List<Lesson>()
        {
            new Lesson("", "", 0, 0, 0, 1, false),
            new Lesson("Matematyka", "Teacher 1", 1, 5, 2),
            new Lesson("Polski", "Teacher 1", 2, 5, 2),
            new Lesson("Biologia", "Teacher 1", 3, 1, 1),
            new Lesson("Chemia", "Teacher 1", 4, 1, 1),
            new Lesson("Angielski", "Teacher 1", 5, 3, 2),
            new Lesson("Historia", "Teacher 1", 6, 1, 1),
            new Lesson("Geografia", "Teacher 1", 7, 1, 1),
            new Lesson("Wychowanie Fizyczne", "Teacher 1", 9, 2, 2),
        };
    }

    public class Lesson
    {
        public string? Title { get; set; }
        public string? Teacher { get; set; }
        public int? Classroom { get; set; }
        public int? inMinimumWeek { get; set; }
        public int? inMaximunDay { get; set; }

        public int? inMiddleMAX { get; set; } = 0;
        public bool isFree { get; set; } = true;

        public Lesson(string title, string teacher, int classroom, int inminimumweek, int inmaximumday, int inmiddlemax = 0, bool active = true)
        {
            Title = title;
            Teacher = teacher;
            Classroom = classroom;
            inMinimumWeek = inminimumweek;
            inMinimumWeek = inmaximumday;
            inMiddleMAX = inmiddlemax;
            isFree = active;
        }

        public override string ToString()
        {
            return (isFree ? $"{Title} {Classroom}\n{Teacher}\n" : $" Godzina zajęciowa wolna :)");
        }
    }

    public class ScheduleOne
    {
        public List<string> ClassName { get; set; } = new List<string>() { "Klasa 1", "Klasa 2", "Klasa 3", "Klasa 4" };

        public List<Lesson> Monday { get; set; } = new List<Lesson>();
        public List<Lesson> Tuesday { get; set; } = new List<Lesson>();
        public List<Lesson> Wednesday { get; set; } = new List<Lesson>();
        public List<Lesson> Thursday { get; set; } = new List<Lesson>();
        public List<Lesson> Friday { get; set; } = new List<Lesson>();
        
        public void RandomiseDay()
        {

        }
    }

    public class Schedule
    {
        public List<ScheduleOne> Classes { get; set; }



    }

    internal class Program
    {
        static void Main(string[] args)
        {
            ScheduleOne test = new ScheduleOne();


        }
    }
}
