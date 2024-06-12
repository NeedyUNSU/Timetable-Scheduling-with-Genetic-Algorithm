using SchedulePlanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI_DevSchedule.test
{
    public static class GlobalValues
    {
        public static List<LessonOne> Lessons = new List<LessonOne>()
        {
            new LessonOne("", 0, 0),
            new LessonOne("Matematyka", 5, 2),
            new LessonOne("Polski", 5, 2),
            new LessonOne("Biologia", 2, 1),
            new LessonOne("Chemia", 2, 1),
            new LessonOne("Angielski", 3, 1),
            new LessonOne("Historia", 2, 1),
            new LessonOne("Geografia", 2, 1),
            new LessonOne("WF", 4, 2),
        };

        public static int MaxDiffrence = 3;

        public static int LessonsInDay = 8;
        public static int WorkingDays = 5;
        public static int DefaultTableCount = 12;
    }

    public class LessonOne
    {
        public string Name { get; set; }
        public int MaxInWeek { get; set; }
        public int MaxInDay { get; set; }

        public int CountDay { get; set; }
        public int CountWeek { get; set; }

        public LessonOne(string name, int maxInWeek, int maxInDay, int countDay = 0, int countWeek = 0)
        {
            Name = name;
            MaxInWeek = maxInWeek;
            MaxInDay = maxInDay;
            CountDay = countDay;
            CountWeek = countWeek;
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }


    public class ScheduleInt
    {
        // punktacja każdej z tabeli im wynik większy tym gorszy jest to plan zajeć 
        public int[] Fitness { get; set; } = new int[GlobalValues.DefaultTableCount];

        public List<LessonOne> LessonsAll = GlobalValues.Lessons;

        public int[,,] schedule { get; set; } = new int[GlobalValues.LessonsInDay, GlobalValues.WorkingDays, GlobalValues.DefaultTableCount];

        public void RandomSchedule()
        {
            int[,,] ints = new int[GlobalValues.LessonsInDay, GlobalValues.WorkingDays, GlobalValues.DefaultTableCount];
            Random rnd = new Random();

            for (int i = 0; i < GlobalValues.LessonsInDay; i++)
                for (int j = 0; j < GlobalValues.WorkingDays; j++)
                    for (int k = 0; k < GlobalValues.DefaultTableCount; k++)
                        ints[i, j, k] = rnd.Next(0, GlobalValues.Lessons.Count);

            schedule = ints;
        }

        public void ShowSchedule()
        {
            int[,,] ints = this.schedule;

            for (int k = 0; k < GlobalValues.DefaultTableCount; k++)
            {
                Console.WriteLine($"\n{Fitness[k]}\n");
                for (int i = 0; i < GlobalValues.LessonsInDay; i++)
                {
                    for (int j = 0; j < GlobalValues.WorkingDays; j++)
                    {
                        Console.Write($"{ints[i, j, k]} {GlobalValues.Lessons[ints[i, j, k]]} ".PadRight(15));
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("\n\n\n");
            }
        }

        private void FitnessDay(int day, int table)
        {
            for (int i = 1; i < GlobalValues.LessonsInDay; i++)
            {
                LessonsAll[schedule[i, day, table]].CountDay++;
            }

            for (int i = 1; i < GlobalValues.LessonsInDay; i++)
            {
                LessonsAll[schedule[i, day, table]].CountWeek += LessonsAll[schedule[i, day, table]].CountDay;

                if (LessonsAll[schedule[i, day, table]].CountDay > LessonsAll[schedule[i, day, table]].MaxInDay) Fitness[table] += (LessonsAll[schedule[i, day, table]].CountDay - LessonsAll[schedule[i, day, table]].MaxInDay) * 4;

                LessonsAll[schedule[i, day, table]].CountDay = 0;
            }
        }

        private void FitnessWeek(int table)
        {
            for (int j = 0; j < GlobalValues.WorkingDays; j++)
            {
                FitnessDay(j, table);
            }

            for (int i = 1; i < LessonsAll.Count; i++)
            {
                if (LessonsAll[i].CountWeek != LessonsAll[i].MaxInWeek)
                {
                    Fitness[table] += Math.Abs(LessonsAll[i].CountWeek - LessonsAll[i].MaxInWeek) * 6;
                }
                LessonsAll[i].CountWeek = 0;
            }
        }

        // generuje wartość błędu zapisaną do zmiennej 
        public void FitnessAll()
        {
            for (int i = 0; i < GlobalValues.DefaultTableCount; i++)
            {
                FitnessWeek(i);

                int[] LessonsCount = new int[GlobalValues.WorkingDays];

                for (int j = 0; j < GlobalValues.WorkingDays; j++)
                {
                    for (int k = 1; k < GlobalValues.LessonsInDay; k++)
                    {
                        if (schedule[k, j, i] != 0) LessonsCount[j]++;
                    }
                }

                if (LessonsCount.Max() - LessonsCount.Min() > GlobalValues.MaxDiffrence)
                {
                    Fitness[i] += (LessonsCount.Max() - LessonsCount.Min() - GlobalValues.MaxDiffrence) * 3;
                }

                if (LessonsCount.Min() == 0)
                {
                    Fitness[i] += 12;
                }

            }
        }

    }

}
