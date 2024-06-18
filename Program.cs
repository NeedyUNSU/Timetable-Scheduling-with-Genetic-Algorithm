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
            //bestSchedule.FitnessAll();
            //bestSchedule.ShowSchedule();

            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine();

            ga.bestInAllGenerations.ShowTrueSchedule();

            //ScheduleInt si = new ScheduleInt();

            //si.RandomSchedule();
            //si.FitnessAll();

            //si.ShowSchedule();


        }
    }
}
