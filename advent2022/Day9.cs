using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent2022
{
    // prev guess: 6426
    public static class Day9
    {
        public static void Exec()
        {
            var directory = "C:/Users/Anna/Documents/Advent/advent2022/advent2022/";
            List<string> input = File.ReadAllLines(directory + "/resources/Day9-1.txt").ToList();

            Puzzle1(input);
           // Puzzle2(input);
        }

        private static void Puzzle1(List<string> input)
        {
            var posVisited = new Dictionary<string, int>();
            var posH = new Pos();
            var posT = new Pos();

            foreach (var item in input)
            {
                var details = item.Split(" ");
                var direction = details.First();
                var steps = int.Parse(details.Last());

                while(steps > 0)
                {
                    steps -= 1;
                    switch (direction)
                    {
                        case "D":
                            posH.Y -= 1;
                            break;
                        case "U":
                            posH.Y += 1;
                            break;
                        case "L":
                            posH.X -= 1;
                            break;
                        case "R":
                            posH.X += 1;
                            break;
                    }

                    var diffX = posH.X - posT.X;
                    var diffY = posH.Y - posT.Y;

                    // H match T
                    if (posH.X == posT.X && posH.Y == posT.Y)
                    {
                        //do nothing
                    } 
                    // H within 1 step from T no need to move
                    else if(diffX >= -1 && diffX <= 1 && diffY >= -1 && diffY <= 1)
                    {
                        // do nothing
                    }
                    else
                    {
                        // move T horizontally
                        if(diffX >= 1)
                        {
                            posT.X += 1;
                        }else if(diffX <= -1)
                        {
                            posT.X -= 1;
                        }

                        // move T vertically
                        if (diffY >= 1)
                        {
                            posT.Y += 1;
                        }
                        else if (diffY <= -1)
                        {
                            posT.Y -= 1;
                        }
                    }

                    var endH = $"{posH.X}{posH.Y}";
                    var pos = $"{posT.X}{posT.Y}";

                    //Console.WriteLine($"posh {endH} - post {pos}");
                    if (posVisited.ContainsKey(pos))
                    {
                        posVisited[pos] += 1;
                    }
                    else
                    {
                        posVisited.Add(pos, 1);
                    }
                }
            }

            Console.WriteLine($"Result 1: {posVisited.Count}");
        }

        private static void Puzzle2(List<string> input)
        {
            
            Console.WriteLine($"Result 2: ");
        }

        private class Pos 
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
    }
}
