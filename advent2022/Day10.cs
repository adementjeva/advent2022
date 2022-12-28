using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent2022
{
    public static class Day10
    {
        private static List<long> signals = new List<long> { 20, 60, 100, 140, 180, 220 };
        private static List<long> rows = new List<long> { 40, 80, 120, 160, 200, 240 };
        public static void Exec()
        {
            var directory = "C:/Users/Anna/Documents/Advent/advent2022/advent2022/";
            List<string> input = File.ReadAllLines(directory + "/resources/Day10-1.txt").ToList();

            //Puzzle1(input);
            Puzzle2(input);
        }

        private static void Puzzle1(List<string> input)
        {
            long amount = 0;
            var cycleCount = 0;
            long X = 1;
            foreach (var item in input)
            {
                var details = item.Split(" ");
                if(details[0] == "noop")
                {
                    cycleCount += 1;
                    amount += CheckCycle(cycleCount, X);
                }
                else
                {
                    cycleCount += 1;
                    amount += CheckCycle(cycleCount, X);
                    cycleCount += 1;
                    amount += CheckCycle(cycleCount, X);
                    X += long.Parse(details[1]);
                    Console.WriteLine(X);
                }

            }
            Console.WriteLine($"Result 1: {amount}");
        }

        private static long CheckCycle(long cycleCount, long X)
        {
            if (signals.Contains(cycleCount))
            {
                //Console.WriteLine($"cycle {cycleCount}, x {X}, multuple, {cycleCount * X}");
                return cycleCount * X;
            }
            return 0;
        }

        private static void Puzzle2(List<string> input)
        {
            var cycleCount = 0;
            long X = 1;
            int row = 0;
            foreach (var item in input)
            {
                var details = item.Split(" ");
                if (details[0] == "noop")
                {
                    var point = cycleCount - (row * 40);

                    if (point == X || point == (X - 1) || point == (X + 1))
                    {
                        Console.Write($"#");
                    }
                    else
                    {
                        Console.Write($".");
                    }

                    cycleCount += 1;
                    if (rows.Contains(cycleCount))
                    {
                        Console.WriteLine("");
                        row += 1;
                    }
                }
                else
                {
                    var point = cycleCount - (row * 40);

                    if (point == X || point == (X - 1) || point == (X + 1))
                    {
                        Console.Write($"#");
                    }
                    else
                    {
                        Console.Write($".");
                    }

                    cycleCount += 1;

                    if (rows.Contains(cycleCount))
                    {
                        Console.WriteLine("");
                        row += 1;
                    }

                    point = cycleCount - (row * 40);
                    if (point == X || point == (X - 1) || point == (X + 1))
                    {
                        Console.Write($"#");
                    }
                    else
                    {
                        Console.Write($".");
                    }
                    
                    cycleCount += 1;

                    if (rows.Contains(cycleCount))
                    {
                        Console.WriteLine("");
                        row += 1;
                    }
                    X += long.Parse(details[1]);
                }

            }
            Console.WriteLine($"Result 2: ");
        }

        private static void DrawPixel() { 
        }
    }
}
