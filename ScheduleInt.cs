using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI_DevSchedule
{
    public class ScheduleInt
    {
        public int[] Fitness { get; set; } = new int[GlobalValues.DefaultTableCount];

        public List<LessonOne> LessonsAll = GlobalValues.Lessons;
        public int[,,] schedule { get; set; } = new int[GlobalValues.LessonsInDay, GlobalValues.WorkingDays, GlobalValues.DefaultTableCount];

        public int MinFitness { get; set; } = int.MaxValue;
        public int[,] Minschedule { get; set; } = new int[GlobalValues.LessonsInDay, GlobalValues.WorkingDays];


        public void RandomSchedule()
        {
            Random rnd = new Random();

            for (int i = 0; i < GlobalValues.LessonsInDay; i++)
                for (int j = 0; j < GlobalValues.WorkingDays; j++)
                    for (int k = 0; k < GlobalValues.DefaultTableCount; k++)
                    {
                        schedule[i, j, k] = GlobalValues.Limits[j][i][rnd.Next(0, GlobalValues.Limits[j][i].Count)];
                    }
        }

        public void ShowSchedule()
        {
            for (int k = 0; k < GlobalValues.DefaultTableCount; k++)
            {
                Console.WriteLine($"\n{Fitness[k] / 2}\n");
                for (int i = 0; i < GlobalValues.LessonsInDay; i++)
                {
                    for (int j = 0; j < GlobalValues.WorkingDays; j++)
                    {
                        Console.Write($"{schedule[i, j, k]} {GlobalValues.Lessons[schedule[i, j, k]]} ".PadRight(15));
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("\n\n\n");
            }
        }

        public void Min()
        {
            for (int k = 0; k < GlobalValues.DefaultTableCount; k++)
            {
                if (MinFitness > Fitness[k])
                {
                    for (int i = 0; i < GlobalValues.LessonsInDay; i++)
                    {
                        for (int j = 0; j < GlobalValues.WorkingDays; j++)
                        {
                            Minschedule[i, j] = schedule[i, j, k];
                        }
                    }
                    MinFitness = Fitness[k];
                }
            }
        }
        public void ShowTrueSchedule()
        {

            Console.WriteLine($"\n{MinFitness / 2}\n");
            for (int i = 0; i < GlobalValues.LessonsInDay; i++)
            {
                for (int j = 0; j < GlobalValues.WorkingDays; j++)
                {
                    Console.Write($"{Minschedule[i, j]} {GlobalValues.Lessons[Minschedule[i, j]]} ".PadRight(15));
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n\n");
        }

        private void FitnessDay(int day, int table, bool show = false)
        {
            int second = -1;

            for (int i = GlobalValues.LessonsInDay - 1; i > 1; i--)
            {
                LessonsAll[schedule[i, day, table]].CountDay++;
                if (schedule[i, day, table] != 0 && second == -1) second = i;
            }

            int first = -1;
            int holeBettwen = 0;

            for (int i = 1; i < GlobalValues.LessonsInDay; i++)
            {
                if (schedule[i, day, table] != 0 && first == -1) first = i;


                LessonsAll[schedule[i, day, table]].CountWeek += LessonsAll[schedule[i, day, table]].CountDay;

                if (LessonsAll[schedule[i, day, table]].CountDay > LessonsAll[schedule[i, day, table]].MaxInDay)
                {
                    Fitness[table] += (LessonsAll[schedule[i, day, table]].CountDay - LessonsAll[schedule[i, day, table]].MaxInDay) * 4;
                    if (GlobalValues.DisplayInfo || show) Console.WriteLine($"Table:{table} Day:{day} {LessonsAll[schedule[i, day, table]].Name} too many a day Points Added: {(LessonsAll[schedule[i, day, table]].CountDay - LessonsAll[schedule[i, day, table]].MaxInDay) * 4}");
                }

                if (first != -1 && i < second && schedule[i, day, table] == 0) holeBettwen++;

                LessonsAll[schedule[i, day, table]].CountDay = 0;
            }

            Fitness[table] += holeBettwen * 2;
            if (holeBettwen != 0) if (GlobalValues.DisplayInfo || show) Console.WriteLine($"Table:{table} Day:{day} Points for hole Added: {holeBettwen * 2}");
        }

        private void FitnessWeek(int table, bool show = false)
        {
            for (int j = 0; j < GlobalValues.WorkingDays; j++)
            {
                FitnessDay(j, table, show);
                if (GlobalValues.DisplayInfo || show) Console.WriteLine();
            }

            for (int i = 1; i < LessonsAll.Count; i++)
            {
                if (LessonsAll[i].CountWeek != LessonsAll[i].MaxInWeek)
                {
                    Fitness[table] += Math.Abs(LessonsAll[i].CountWeek - LessonsAll[i].MaxInWeek) * 6;
                    if (GlobalValues.DisplayInfo || show) Console.WriteLine($"Table:{table} {LessonsAll[i].Name} too low or too high a week Added: {Math.Abs(LessonsAll[i].CountWeek - LessonsAll[i].MaxInWeek) * 6}");
                }
                LessonsAll[i].CountWeek = 0;
            }
        }

        public void FitnessAll(bool show = false)
        {
            for (int i = 0; i < GlobalValues.DefaultTableCount; i++)
            {
                FitnessWeek(i, show);
                if (GlobalValues.DisplayInfo || show) Console.WriteLine();

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
                    if (GlobalValues.DisplayInfo || show) Console.WriteLine($"Table:{i} Difference bettwen days too high Points Added: {(LessonsCount.Max() - LessonsCount.Min() - GlobalValues.MaxDiffrence) * 3}");
                }

                if (LessonsCount.Min() == 0)
                {
                    Fitness[i] += 12;
                    if (GlobalValues.DisplayInfo || show) Console.WriteLine($"Table:{i} has empty day");
                }
            }

            if (GlobalValues.DisplayInfo || show) Console.WriteLine("\n\n\n\n");
        }

        public ScheduleInt Crossover(ScheduleInt partner)
        {
            ScheduleInt child = new ScheduleInt();
            Random rnd = new Random();
            int crossoverPoint = rnd.Next(GlobalValues.LessonsInDay);

            for (int i = 0; i < GlobalValues.LessonsInDay; i++)
            {
                for (int j = 0; j < GlobalValues.WorkingDays; j++)
                {
                    for (int k = 0; k < GlobalValues.DefaultTableCount; k++)
                    {
                        if (i < crossoverPoint)
                        {
                            child.schedule[i, j, k] = this.schedule[i, j, k];
                        }
                        else
                        {
                            child.schedule[i, j, k] = partner.schedule[i, j, k];
                        }
                    }
                }
            }

            return child;
        }

        public void Mutate()
        {
            Random rnd = new Random();

            for (int i = 0; i < GlobalValues.LessonsInDay; i++)
            {
                for (int j = 0; j < GlobalValues.WorkingDays; j++)
                {
                    for (int k = 0; k < GlobalValues.DefaultTableCount; k++)
                    {
                        if (rnd.NextDouble() < GlobalValues.MutationRate)
                        {
                            schedule[i, j, k] = GlobalValues.Limits[j][i][rnd.Next(0, GlobalValues.Limits[j][i].Count)];
                        }
                    }
                }
            }
        }
    }
}
