using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent2022
{
    public static class Day1
    {
        public static void Exec()
        {
            var directory = "C:/Users/Anna/Documents/Advent/advent2022/advent2022/";
            List<string> input = File.ReadAllLines(directory + "/resources/Day1-1.txt").ToList();

            Puzzle1(input);
            Puzzle2(input);
        }

        private static void Puzzle1(List<string> input)
        {
            long highest = 0;
            long sum = 0;
            foreach (var item in input)
            {
                if(string.IsNullOrEmpty(item))
                {
                    if (sum > highest)
                    {
                        highest = sum;
                    }
                    sum = 0;
                    continue;
                }

                var num = long.Parse(item);
                sum += num;
            }

            Console.WriteLine("highest: " + highest);
        }
        
        private static void Puzzle2(List<string> input)
        {
            long highest1 = 0;
            long highest2 = 0;
            long highest3 = 0;
            long sum = 0;
            foreach (var item in input)
            {
                if (string.IsNullOrEmpty(item))
                {
                    if (sum > highest1)
                    {
                        highest3 = highest2;
                        highest2 = highest1;
                        highest1 = sum;
                        sum = 0;
                        continue;
                    }

                    if (sum > highest2)
                    {
                        highest3 = highest2;
                        highest2 = sum;
                        sum = 0;
                        continue;
                    }

                    if (sum > highest3)
                    {
                        highest3 = sum;
                        sum = 0;
                        continue;
                    }
                    sum = 0;
                    continue;
                }

                var num = long.Parse(item);
                sum += num;
            }
            var total = highest1 + highest2 + highest3;
            Console.WriteLine("highest: " + total);
        }
    }
}
