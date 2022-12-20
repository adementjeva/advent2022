using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent2022
{
    public static class DayX
    {
        public static void Exec()
        {
            var directory = "C:/Users/Anna/Documents/Advent/advent2022/advent2022/";
            List<string> input = File.ReadAllLines(directory + "/resources/DayX-1.txt").ToList();

            Puzzle1(input);
           // Puzzle2(input);
        }

        private static void Puzzle1(List<string> input)
        {
            
            Console.WriteLine($"Result 1: ");
        }

        private static void Puzzle2(List<string> input)
        {
            
            Console.WriteLine($"Result 2: ");
        }
    }
}
