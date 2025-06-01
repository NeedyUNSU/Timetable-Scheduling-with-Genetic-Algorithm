using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SI_DevSchedule
{
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
