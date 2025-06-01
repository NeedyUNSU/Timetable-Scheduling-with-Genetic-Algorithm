using SI_DevSchedule;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchedulePlanner
{
    class Program
    {
        static void Main(string[] args)
        {
            GeneticAlgorithm ga = new GeneticAlgorithm();
            ScheduleInt bestSchedule = ga.Evolve();
            Console.WriteLine("\n\nCurrent schedule");
            ga.bestInAllGenerations.ShowTrueSchedule();
        }
    }
}
