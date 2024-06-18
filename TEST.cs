using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SI_DevSchedule
{
    public static class GlobalValues
    {
        public static List<LessonOne> Lessons = new List<LessonOne>()
        {
            new LessonOne("", 100, 10),        // 0
            new LessonOne("Matematyka", 5, 2), // 1
            new LessonOne("Polski", 4, 2),     // 2
            new LessonOne("Biologia", 2, 1),   // 3
            new LessonOne("Chemia", 2, 1),     // 4 
            new LessonOne("Angielski", 3, 1),  // 5
            new LessonOne("Historia", 1, 1),   // 6
            new LessonOne("Geografia", 2, 1),  // 7
            new LessonOne("WF", 3, 2),         // 8
        };


        public static int MaxDiffrence = 3;

        public static int LessonsInDay = 8;
        public static int WorkingDays = 5;
        public static int DefaultTableCount = 15;

        public static int PopulationSize = 500;
        public static int Generations = 10;
        public static double MutationRate = 0.5;

        public static bool DisplayInfo = false;

        public static List<List<List<int>>> Limits = new List<List<List<int>>>()
        {
            new List<List<int>> {
                new List<int>() { 0, 5, 7, 8 },
                new List<int>() { 0, 3, 5, 6, 8 },
                new List<int>() { 0, 1, 2, 3, 5, 6 },
                new List<int>() { 0, 1, 4 },
                new List<int>() { 0, 4 },
                new List<int>() { 0 },
                new List<int>() { 0 },
                new List<int>() { 0 },
            },
            new List<List<int>> {
                new List<int>() { 0, 8 },
                new List<int>() { 0, 3 },
                new List<int>() { 0, 3, 4 },
                new List<int>() { 0, 4 },
                new List<int>() { 0 },
                new List<int>() { 0 },
                new List<int>() { 0 },
                new List<int>() { 0 },
            },
            new List<List<int>> {
                new List<int>() { 0, 5, 7, 8 },
                new List<int>() { 0, 6, 8 },
                new List<int>() { 0, 2, 6, 8 },
                new List<int>() { 0, 1, 2, 8 },
                new List<int>() { 0, 1, 8 },
                new List<int>() { 0, 8 },
                new List<int>() { 0 },
                new List<int>() { 0 },
            },
            new List<List<int>> {
                new List<int>() { 0, 6, 7, 8 },
                new List<int>() { 0, 1, 3, 5, 6, 7, 8 },
                new List<int>() { 0, 1, 2 },
                new List<int>() { 0, 2 },
                new List<int>() { 0 },
                new List<int>() { 0 },
                new List<int>() { 0 },
                new List<int>() { 0 },
            },
            new List<List<int>> {
                new List<int>() { 0, 4, 5 },
                new List<int>() { 0, 2, 3, 4, 5 },
                new List<int>() { 0, 1, 2, 8 },
                new List<int>() { 0, 7, 8 },
                new List<int>() { 0, 7, 8 },
                new List<int>() { 0 },
                new List<int>() { 0 },
                new List<int>() { 0 },
            },
        };

    }

    public class LessonOne
    {
        public string Name { get; set; }
        public int MaxInWeek { get; set; }
        public int MaxInDay { get; set; }

        public int CountDay { get; set; }
        public int CountWeek { get; set; }

        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        /// <param name="name"> Nazwa przedmiotu</param>
        /// <param name="maxInWeek"> maksymalne wystąpienie w tygodniu </param>
        /// <param name="maxInDay"> maksumalne wystąpienie w ciągu dnia</param>
        /// <param name="countDay"> zliczanie ile w ciągu dnia</param>
        /// <param name="countWeek"> zliczanie ile w ciągu tygodnia </param>
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
                Console.WriteLine($"\n{Fitness[k]/2}\n");
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

            Console.WriteLine($"\n{MinFitness/2}\n");
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

    public class GeneticAlgorithm
    {
        public List<ScheduleInt> Population { get; set; } = new List<ScheduleInt>();
        public ScheduleInt bestInAllGenerations = new ScheduleInt();

        public GeneticAlgorithm()
        {
            for (int i = 0; i < GlobalValues.PopulationSize; i++)
            {
                ScheduleInt individual = new ScheduleInt();
                individual.RandomSchedule();
                Population.Add(individual);
            }
        }

        public ScheduleInt Evolve()
        {
            for (int generation = 0; generation < GlobalValues.Generations; generation++)
            {
                foreach (var individual in Population)
                {
                    individual.FitnessAll();
                    if (GlobalValues.DisplayInfo) Console.WriteLine(individual.Fitness.Sum(x => Convert.ToInt32(x)));
                }

                Population = Population.OrderBy(ind => ind.Fitness.Sum()).ToList();

                List<ScheduleInt> newPopulation = new List<ScheduleInt>();

                for (int i = 0; i < GlobalValues.PopulationSize / 2; i++)
                {
                    ScheduleInt parent1 = Population[i];
                    ScheduleInt parent2 = Population[GlobalValues.PopulationSize - i - 1];

                    ScheduleInt child1 = parent1.Crossover(parent2);
                    ScheduleInt child2 = parent2.Crossover(parent1);

                    child1.Mutate();
                    child2.Mutate();

                    child1.FitnessAll();
                    child2.FitnessAll();
                    child1.Min();
                    if (child1.MinFitness < bestInAllGenerations.MinFitness)
                    {
                        bestInAllGenerations.Minschedule = child1.Minschedule;
                        bestInAllGenerations.MinFitness = child1.MinFitness;
                    }

                    child2.Min();
                    if (child2.MinFitness < bestInAllGenerations.MinFitness)
                    {
                        bestInAllGenerations.Minschedule = child2.Minschedule;
                        bestInAllGenerations.MinFitness = child2.MinFitness;
                    }

                    newPopulation.Add(child1);
                    newPopulation.Add(child2);
                    newPopulation.ForEach(x => x.FitnessAll());
                }

                Population = newPopulation;
            }

            Console.WriteLine(Population.OrderBy(ind => ind.Fitness.Sum()).First().Fitness.Sum(x => Convert.ToInt32(x)));

            return Population.OrderBy(ind => ind.Fitness.Sum()).First();
        }
    }


}
