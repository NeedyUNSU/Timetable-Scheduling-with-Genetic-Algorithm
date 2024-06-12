using SI_DevSchedule;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchedulePlanner
{
    //public static class Glob
    //{
    //    public static List<Lesson> DefultLessons = new List<Lesson>()
    //    {
    //        new Lesson("", "", 0, 0, 0, 1, false),
    //        new Lesson("Matematyka", "Teacher 1", 1, 5, 2),
    //        new Lesson("Polski", "Teacher 2", 2, 5, 2),
    //        new Lesson("Biologia", "Teacher 3", 3, 1, 1),
    //        new Lesson("Chemia", "Teacher 4", 4, 1, 1),
    //        new Lesson("Angielski", "Teacher 5", 5, 3, 2),
    //        new Lesson("Historia", "Teacher 6", 6, 1, 1),
    //        new Lesson("Geografia", "Teacher 7", 7, 1, 1),
    //        new Lesson("WF", "Teacher 8", 8, 2, 2),
    //    };
    //}

    //public class Lesson
    //{
    //    public string Title { get; set; }
    //    public string Teacher { get; set; }
    //    public int Classroom { get; set; }
    //    public int inMinimumWeek { get; set; }
    //    public int inMaximunDay { get; set; }
    //    public int inMiddleMAX { get; set; } = 0;
    //    public bool isFree { get; set; } = true;

    //    public Lesson(string title, string teacher, int classroom, int inminimumweek, int inmaximumday, int inmiddlemax = 0, bool active = true)
    //    {
    //        Title = title;
    //        Teacher = teacher;
    //        Classroom = classroom;
    //        inMinimumWeek = inminimumweek;
    //        inMaximunDay = inmaximumday;
    //        inMiddleMAX = inmiddlemax;
    //        isFree = active;
    //    }

    //    public override string ToString()
    //    {
    //        return (isFree ? $"{Title}" : " Free :)");
    //    }
    //}

    //public class ScheduleOne
    //{
    //    public List<Lesson> Monday { get; set; } = new List<Lesson>();
    //    public List<Lesson> Tuesday { get; set; } = new List<Lesson>();
    //    public List<Lesson> Wednesday { get; set; } = new List<Lesson>();
    //    public List<Lesson> Thursday { get; set; } = new List<Lesson>();
    //    public List<Lesson> Friday { get; set; } = new List<Lesson>();

    //    public List<Lesson> RandomiseDay()
    //    {
    //        List<Lesson> outL = new List<Lesson>();
    //        Random rnd = new Random();
    //        int numberOfLessons = rnd.Next(1, 9); // Losowa liczba zajęć w dniu (4 do 8)

    //        for (int i = 0; i < numberOfLessons; i++)
    //        {
    //            outL.Add(Glob.DefultLessons[rnd.Next(0, Glob.DefultLessons.Count)]);
    //        }

    //        return outL;
    //    }

    //    public void RandomizeWeek()
    //    {
    //        Monday = RandomiseDay();
    //        Tuesday = RandomiseDay();
    //        Wednesday = RandomiseDay();
    //        Thursday = RandomiseDay();
    //        Friday = RandomiseDay();
    //    }

    //    public void ShowSchedule()
    //    {
    //        string[] days = { "Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek" };
    //        List<Lesson>[] week = { Monday, Tuesday, Wednesday, Thursday, Friday };

    //        Console.WriteLine("Plan zajęć:");
    //        Console.WriteLine("--------------------------------------------------------------------------------------------------");
    //        Console.WriteLine("| Numer | Poniedziałek    | Wtorek           | Środa          | Czwartek        | Piątek          |");
    //        Console.WriteLine("--------------------------------------------------------------------------------------------------");

    //        int maxLessons = week.Max(d => d.Count);

    //        for (int hour = 0; hour < maxLessons; hour++)
    //        {
    //            Console.Write($"|   {hour + 1}   |");

    //            for (int day = 0; day < 5; day++)
    //            {
    //                if (hour < week[day].Count)
    //                {
    //                    string lesson = week[day][hour].ToString().Replace("\n", " ");
    //                    Console.Write($" {lesson.PadRight(15)} |");
    //                }
    //                else
    //                {
    //                    Console.Write("                 |");
    //                }
    //            }

    //            Console.WriteLine();
    //            Console.WriteLine("--------------------------------------------------------------------------------------------------");
    //        }
    //    }
    //}

    //public class Schedule
    //{
    //    public List<string> ClassName { get; set; } = new List<string>() { "Klasa 1", "Klasa 2", "Klasa 3", "Klasa 4" };
    //    public List<ScheduleOne> Classes { get; set; } = new List<ScheduleOne>();

    //    public Schedule()
    //    {
    //        foreach (var className in ClassName)
    //        {
    //            ScheduleOne scheduleOne = new ScheduleOne();
    //            scheduleOne.RandomizeWeek();
    //            Classes.Add(scheduleOne);
    //        }
    //    }

    //    public void ShowSchedules()
    //    {
    //        for (int i = 0; i < Classes.Count; i++)
    //        {
    //            Console.WriteLine(ClassName[i]);
    //            Classes[i].ShowSchedule();
    //            Console.WriteLine();
    //        }
    //    }
    //}

    //public class GeneticAlgorithm
    //{
    //    public Schedule OptimizeSchedule(Schedule schedule, int populationSize, int generations, double mutationRate)
    //    {
    //        List<Schedule> population = new List<Schedule>();
    //        Random rnd = new Random();

    //        for (int i = 0; i < populationSize; i++)
    //        {
    //            Schedule newSchedule = new Schedule();
    //            population.Add(newSchedule);
    //        }

    //        for (int generation = 0; generation < generations; generation++)
    //        {
    //            population = population.OrderBy(s => Fitness(s)).ToList();

    //            List<Schedule> newPopulation = new List<Schedule>();

    //            for (int i = 0; i < populationSize / 2; i++)
    //            {
    //                Schedule parent1 = population[rnd.Next(0, populationSize / 2)];
    //                Schedule parent2 = population[rnd.Next(0, populationSize / 2)];
    //                Schedule child = Crossover(parent1, parent2);
    //                Mutate(child, mutationRate);
    //                newPopulation.Add(child);
    //            }

    //            population.AddRange(newPopulation);
    //            population = population.OrderBy(s => Fitness(s)).Take(populationSize).ToList();
    //        }

    //        return population.OrderBy(s => Fitness(s)).First();
    //    }

    //    public int Fitness(Schedule schedule)
    //    {
    //        int fitness = 0;

    //        // Sprawdzanie wszystkich klas i dni tygodnia
    //        for (int classIndex = 0; classIndex < schedule.Classes.Count; classIndex++)
    //        {
    //            var cls = schedule.Classes[classIndex];
    //            fitness += CalculateFitnessForWeek(cls, schedule, classIndex);
    //        }

    //        return fitness;
    //    }

    //    private int CalculateFitnessForWeek(ScheduleOne cls, Schedule schedule, int classIndex)
    //    {
    //        int fitness = 0;
    //        Dictionary<string, int> weeklyLessonCount = new Dictionary<string, int>();

    //        List<Lesson>[] week = { cls.Monday, cls.Tuesday, cls.Wednesday, cls.Thursday, cls.Friday };

    //        for (int dayIndex = 0; dayIndex < week.Length; dayIndex++)
    //        {
    //            fitness += CalculateFitnessForDay(week[dayIndex], schedule, classIndex);

    //            foreach (var lesson in week[dayIndex])
    //            {
    //                if (lesson.isFree) continue;

    //                if (!weeklyLessonCount.ContainsKey(lesson.Title))
    //                {
    //                    weeklyLessonCount[lesson.Title] = 0;
    //                }

    //                weeklyLessonCount[lesson.Title]++;
    //            }
    //        }

    //        foreach (var lesson in weeklyLessonCount)
    //        {
    //            var defLesson = Glob.DefultLessons.FirstOrDefault(l => l.Title == lesson.Key);
    //            if (defLesson != null && lesson.Value > defLesson.inMinimumWeek)
    //            {
    //                fitness++;
    //            }
    //        }

    //        return fitness;
    //    }

    //    private int CalculateFitnessForDay(List<Lesson> day, Schedule schedule, int classIndex)
    //    {
    //        int fitness = 0;
    //        Dictionary<string, int> lessonCount = new Dictionary<string, int>();

    //        for (int i = 0; i < day.Count; i++)
    //        {
    //            if (day[i].isFree) continue;

    //            if (!lessonCount.ContainsKey(day[i].Title))
    //            {
    //                lessonCount[day[i].Title] = 0;
    //            }

    //            lessonCount[day[i].Title]++;

    //            if (lessonCount[day[i].Title] > day[i].inMaximunDay)
    //            {
    //                fitness++;
    //            }

    //            if (i > 0 && day[i].Title == day[i - 1].Title && day[i].Title != "")
    //            {
    //                fitness++;
    //            }

    //            for (int otherClassIndex = 0; otherClassIndex < schedule.Classes.Count; otherClassIndex++)
    //            {
    //                if (otherClassIndex == classIndex) continue;

    //                var otherClassDay = GetDaySchedule(schedule.Classes[otherClassIndex], day);

    //                if (i < otherClassDay.Count && day[i].Teacher == otherClassDay[i].Teacher)
    //                {
    //                    fitness++;
    //                }
    //            }
    //        }

    //        return fitness;
    //    }

    //    private List<Lesson> GetDaySchedule(ScheduleOne schedule, List<Lesson> day)
    //    {
    //        if (day == schedule.Monday) return schedule.Monday;
    //        if (day == schedule.Tuesday) return schedule.Tuesday;
    //        if (day == schedule.Wednesday) return schedule.Wednesday;
    //        if (day == schedule.Thursday) return schedule.Thursday;
    //        return schedule.Friday;
    //    }

    //    private Schedule Crossover(Schedule parent1, Schedule parent2)
    //    {
    //        Schedule child = new Schedule();

    //        for (int i = 0; i < parent1.Classes.Count; i++)
    //        {
    //            if (i % 2 == 0)
    //            {
    //                child.Classes[i] = parent1.Classes[i];
    //            }
    //            else
    //            {
    //                child.Classes[i] = parent2.Classes[i];
    //            }
    //        }

    //        return child;
    //    }

    //    private void Mutate(Schedule schedule, double mutationRate)
    //    {
    //        Random rnd = new Random();

    //        foreach (var cls in schedule.Classes)
    //        {
    //            if (rnd.NextDouble() < mutationRate)
    //            {
    //                cls.RandomizeWeek();
    //            }
    //        }
    //    }
    //}

    class Program
    {
        static void Main(string[] args)
        {
            //Schedule initialSchedule = new Schedule();
            //GeneticAlgorithm ga = new GeneticAlgorithm();

            //Schedule optimizedSchedule = ga.OptimizeSchedule(initialSchedule, 200, 1000, 0.08);

            //optimizedSchedule.ShowSchedules();

            GeneticAlgorithm ga = new GeneticAlgorithm();
            ScheduleInt bestSchedule = ga.Evolve();

            bestSchedule.ShowSchedule();


            //ScheduleInt si = new ScheduleInt();

            //si.RandomSchedule();
            //si.FitnessAll();

            //si.ShowSchedule();


        }
    }
}
