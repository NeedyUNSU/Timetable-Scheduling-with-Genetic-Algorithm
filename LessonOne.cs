using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI_DevSchedule
{
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
}
