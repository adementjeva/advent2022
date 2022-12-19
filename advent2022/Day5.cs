using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent2022
{
    public static class Day5
    {
        /*
            [C]         [S] [H]                
            [F] [B]     [C] [S]     [W]        
            [B] [W]     [W] [M] [S] [B]        
            [L] [H] [G] [L] [P] [F] [Q]        
            [D] [P] [J] [F] [T] [G] [M] [T]    
            [P] [G] [B] [N] [L] [W] [P] [W] [R]
            [Z] [V] [W] [J] [J] [C] [T] [S] [C]
            [S] [N] [F] [G] [W] [B] [H] [F] [N]
             1   2   3   4   5   6   7   8   9  
         */

        public static void Exec()
        {
            var directory = "C:/Users/Anna/Documents/Advent/advent2022/advent2022/";
            List<string> input = File.ReadAllLines(directory + "/resources/Day5-1.txt").ToList();

            Puzzle1(input);
            Puzzle2(input);
        }

        private static void Puzzle1(List<string> input)
        {
            var dict = new Dictionary<int, List<string>>();

            dict.Add(1, new List<string> { "S", "Z", "P", "D", "L", "B", "F", "C" });
            dict.Add(2, new List<string> { "N", "V", "G", "P", "H", "W", "B" });
            dict.Add(3, new List<string> { "F", "W", "B", "J", "G" });
            dict.Add(4, new List<string> { "G", "J", "N", "F", "L", "W", "C", "S" });
            dict.Add(5, new List<string> { "W", "J", "L", "T", "P", "M", "S", "H" });
            dict.Add(6, new List<string> { "B", "C", "W", "G", "F", "S" });
            dict.Add(7, new List<string> { "H", "T", "P", "M", "Q", "B", "W" });
            dict.Add(8, new List<string> { "F", "S", "W", "T" });
            dict.Add(9, new List<string> { "N", "C", "R" });

            foreach (var item in input)
            {
                //move 2 from 5 to 9
                var amount = int.Parse(item.Split("from").First().Split(" ", StringSplitOptions.RemoveEmptyEntries).Last());
                var initPos = int.Parse(item.Split("from").Last().Split(" ", StringSplitOptions.RemoveEmptyEntries).First());
                var finalPos = int.Parse(item.Split("to").Last().Trim());

                while(amount > 0)
                {
                    var toMove = dict[initPos].Last();
                    dict[finalPos].Add(toMove);
                    dict[initPos].RemoveAt(dict[initPos].Count() - 1);
                    amount -= 1;
                }
            }

            var code = "";
            foreach (var item in dict)
            {
                code += item.Value.Last();
            }
            Console.WriteLine("Result 1: " + code);
        }


        private static void Puzzle2(List<string> input)
        {
            var dict = new Dictionary<int, List<string>>();

            dict.Add(1, new List<string> { "S", "Z", "P", "D", "L", "B", "F", "C" });
            dict.Add(2, new List<string> { "N", "V", "G", "P", "H", "W", "B" });
            dict.Add(3, new List<string> { "F", "W", "B", "J", "G" });
            dict.Add(4, new List<string> { "G", "J", "N", "F", "L", "W", "C", "S" });
            dict.Add(5, new List<string> { "W", "J", "L", "T", "P", "M", "S", "H" });
            dict.Add(6, new List<string> { "B", "C", "W", "G", "F", "S" });
            dict.Add(7, new List<string> { "H", "T", "P", "M", "Q", "B", "W" });
            dict.Add(8, new List<string> { "F", "S", "W", "T" });
            dict.Add(9, new List<string> { "N", "C", "R" });

            foreach (var item in input)
            {
                //move 2 from 5 to 9
                var amount = int.Parse(item.Split("from").First().Split(" ", StringSplitOptions.RemoveEmptyEntries).Last());
                var initPos = int.Parse(item.Split("from").Last().Split(" ", StringSplitOptions.RemoveEmptyEntries).First());
                var finalPos = int.Parse(item.Split("to").Last().Trim());

                var toMove = dict[initPos].TakeLast(amount);
                dict[finalPos].AddRange(toMove);

                while (amount > 0)
                {
                    dict[initPos].RemoveAt(dict[initPos].Count() - 1);
                    amount -= 1;
                }
            }

            var code = "";
            foreach (var item in dict)
            {
                code += item.Value.Last();
            }
            Console.WriteLine("Result 2: " + code);
        }
    }
}
