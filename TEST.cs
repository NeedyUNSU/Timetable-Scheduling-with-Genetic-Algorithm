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
        new LessonOne("", 100, 10),
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
        public static int DefaultTableCount = 15;

        public static int PopulationSize = 50;
        public static int Generations = 100;
        public static double MutationRate = 0.1;
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
        public int[] Fitness { get; set; } = new int[GlobalValues.DefaultTableCount];

        public List<LessonOne> LessonsAll = GlobalValues.Lessons;

        public int[,,] schedule { get; set; } = new int[GlobalValues.LessonsInDay, GlobalValues.WorkingDays, GlobalValues.DefaultTableCount];

        public void RandomSchedule()
        {
            Random rnd = new Random();

            for (int i = 0; i < GlobalValues.LessonsInDay; i++)
                for (int j = 0; j < GlobalValues.WorkingDays; j++)
                    for (int k = 0; k < GlobalValues.DefaultTableCount; k++)
                        schedule[i, j, k] = rnd.Next(0, GlobalValues.Lessons.Count);
        }

        public void ShowSchedule()
        {
            for (int k = 0; k < GlobalValues.DefaultTableCount; k++)
            {
                Console.WriteLine($"\n{Fitness[k]}\n");
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

        private void FitnessDay(int day, int table)
        {
            int second = -1;
            
            for (int i = GlobalValues.LessonsInDay - 1; i >= 1; i--)
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
                    Console.WriteLine($"Table:{table} Day:{day} {LessonsAll[schedule[i, day, table]].Name} too many a day Points Added: {(LessonsAll[schedule[i, day, table]].CountDay - LessonsAll[schedule[i, day, table]].MaxInDay) * 4}");
                }

                if(first != -1 && i <= second && schedule[i, day, table] == 0) holeBettwen++;

                LessonsAll[schedule[i, day, table]].CountDay = 0;
            }

            Fitness[table] += holeBettwen * 2;
            if (holeBettwen != 0) Console.WriteLine($"Table:{table} Day:{day} Points for hole Added: {holeBettwen * 2}");
        }

        private void FitnessWeek(int table)
        {
            for (int j = 0; j < GlobalValues.WorkingDays; j++)
            {
                FitnessDay(j, table);
                Console.WriteLine();
            }

            for (int i = 1; i < LessonsAll.Count; i++)
            {
                if (LessonsAll[i].CountWeek != LessonsAll[i].MaxInWeek)
                {
                    Fitness[table] += Math.Abs(LessonsAll[i].CountWeek - LessonsAll[i].MaxInWeek) * 6;
                    Console.WriteLine($"Table:{table} Points over {LessonsAll[i].Name} a week Added: {Math.Abs(LessonsAll[i].CountWeek - LessonsAll[i].MaxInWeek) * 6}");
                }
                LessonsAll[i].CountWeek = 0;
            }
        }

        public void FitnessAll()
        {
            for (int i = 0; i < GlobalValues.DefaultTableCount; i++)
            {
                FitnessWeek(i);
                Console.WriteLine();

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
                    Console.WriteLine($"Table:{i} Points between certain limits Added: {(LessonsCount.Max() - LessonsCount.Min() - GlobalValues.MaxDiffrence) * 3}");
                }

                if (LessonsCount.Min() == 0)
                {
                    Fitness[i] += 12;
                    Console.WriteLine($"Table:{i} has empty day");
                }
            }

            Console.WriteLine("\n\n\n\n");
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
                            schedule[i, j, k] = rnd.Next(0, GlobalValues.Lessons.Count);
                        }
                    }
                }
            }
        }
    }

    public class GeneticAlgorithm
    {
        public List<ScheduleInt> Population { get; set; } = new List<ScheduleInt>();

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
                    Console.WriteLine(individual.Fitness.Sum(x => Convert.ToInt32(x)));
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
