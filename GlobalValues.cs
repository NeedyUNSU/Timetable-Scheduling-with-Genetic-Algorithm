using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static int PopulationSize = 100;
        public static int Generations = 100;
        public static double MutationRate = 0.2;

        public static bool DisplayInfo = true;

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
}
