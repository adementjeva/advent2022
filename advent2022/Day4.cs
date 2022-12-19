using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent2022
{
    public static class Day4
    {
        public static void Exec()
        {
            var directory = "C:/Users/Anna/Documents/Advent/advent2022/advent2022/";
            List<string> input = File.ReadAllLines(directory + "/resources/Day4-1.txt").ToList();

            Puzzle1(input);
            Puzzle2(input);
        }

        private static void Puzzle1(List<string> input)
        {
            var count = 0;

            foreach (var item in input)
            {
                var first = item.Split(",").First();
                var last = item.Split(",").Last();

                var f1 = long.Parse(first.Split("-").First());
                var f2 = long.Parse(first.Split("-").Last());
                var l1 = long.Parse(last.Split("-").First());
                var l2 = long.Parse(last.Split("-").Last());

                if (f1 >= l1 && f2 <= l2) count += 1;
                else if (l1 >= f1 && l2 <= f2) count += 1;
            }

            Console.WriteLine("Result 1: " + count);
        }


        private static void Puzzle2(List<string> input)
        {
            var count = 0;

            foreach (var item in input)
            {
                var first = item.Split(",").First();
                var last = item.Split(",").Last();

                var f1 = long.Parse(first.Split("-").First());
                var f2 = long.Parse(first.Split("-").Last());
                var l1 = long.Parse(last.Split("-").First());
                var l2 = long.Parse(last.Split("-").Last());

                if (f1 >= l1 && f2 <= l2) count += 1; // first include all last
                else if (l1 >= f1 && l2 <= f2) count += 1; // last incluse all first
                else if (f1<= l1 && f2 >= l1 && f2 < l2) count += 1; // first include some beginning of last
                else if (l1 <= f1 && l2 >= f1 && l2 < f2) count += 1; // last include some beginning of first
                else if (f1 <= l2 && f2 >= l2 && f1 > l1) count += 1; // first include some end of last
                else if (l1 <= f2 && l2 >= f2 && l1 > f1) count += 1; // last include some end of first
            }

            Console.WriteLine("Result 2: " + count);
        }
    }
}
