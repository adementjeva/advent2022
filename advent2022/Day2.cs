using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent2022
{
    //1 for Rock, 2 for Paper, and 3 for Scissors
    // A for Rock, B for Paper, and C for Scissors
    // X for Rock, Y for Paper, and Z for Scissors
    // 0 if you lost, 3 if the round was a draw, and 6 if you won
    public static class Day2
    {
        public static void Exec()
        {
            var directory = "C:/Users/Anna/Documents/Advent/advent2022/advent2022/";
            List<string> input = File.ReadAllLines(directory + "/resources/Day2-1.txt").ToList();

            Puzzle1(input);
            Puzzle2(input);
        }

        private static void Puzzle1(List<string> input)
        {
            var score = 0;
            foreach (var item in input)
            {

                var p1 = item.Split(" ").First();
                var p2 = item.Split(" ").Last();

                // Rock
                if (p1 == "A" && p2 == "X") score += 4; // Rock + Draw
                if (p1 == "A" && p2 == "Y") score += 8; // Paper + Win
                if (p1 == "A" && p2 == "Z") score += 3; // Scissors + Loose
                // Paper
                if (p1 == "B" && p2 == "X") score += 1; // Rock + Loose
                if (p1 == "B" && p2 == "Y") score += 5; // Paper + Draw
                if (p1 == "B" && p2 == "Z") score += 9; // Scissors + Win

                // Scissors
                if (p1 == "C" && p2 == "X") score += 7; // Rock + Win
                if (p1 == "C" && p2 == "Y") score += 2; // Paper + Loose
                if (p1 == "C" && p2 == "Z") score += 6; // Scissors + Draw
            }

            Console.WriteLine("highest: " + score);
        }

        //1 for Rock, 2 for Paper, and 3 for Scissors
        // X means you need to lose, Y means you need to end the round in a draw, and Z means you need to win
        private static void Puzzle2(List<string> input)
        {
            long score = 0;
            foreach (var item in input)
            {
                var p1 = item.Split(" ").First();
                var p2 = item.Split(" ").Last();

                // Rock
                if (p1 == "A" && p2 == "X") score += 3; // Loose + Scissors
                if (p1 == "A" && p2 == "Y") score += 4; // Draw + Rock
                if (p1 == "A" && p2 == "Z") score += 8; // Win + Paper
                // Paper
                if (p1 == "B" && p2 == "X") score += 1; // Loose + Rock
                if (p1 == "B" && p2 == "Y") score += 5; // Draw + Paper
                if (p1 == "B" && p2 == "Z") score += 9; // Win + Scissors
                // Scissors
                if (p1 == "C" && p2 == "X") score += 2; // Loose + paper
                if (p1 == "C" && p2 == "Y") score += 6; // Draw + scissors
                if (p1 == "C" && p2 == "Z") score += 7; // Win + rock
            }

            Console.WriteLine("highest: " + score);
        }
    }
}
