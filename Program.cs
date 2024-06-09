namespace SI_DevSchedule
{
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
