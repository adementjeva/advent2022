using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent2022
{
    public static class Day12
    {
        public static string azAZ = "SabcdefghijklmnopqrstuvwxyzE"; //28
        public static Dictionary<char, int> priorityScore;

        public static void Exec()
        {
            var directory = "C:/Users/Anna/Documents/Advent/advent2022/advent2022/";
            List<string> input = File.ReadAllLines(directory + "/resources/Day12-1.txt").ToList();
            
            priorityScore = new Dictionary<char, int>();
            var letters = azAZ.ToCharArray();

            for (int i = 0; i < letters.Length; i++)
            {
                priorityScore.Add(letters[i], i);
            }

            Puzzle1(input);
           // Puzzle2(input);
        }

        private static void Puzzle1(List<string> input)
        {
            var coordinates = new Dictionary<string, Cor>();
            var targetCor = new Cor();
            var startCor = new Cor();

            for (int i = 0; i < input.Count; i++)
            {
                var details = input[i].ToCharArray();

                for (int j = 0; j < details.Length; j++)
                {
                    coordinates.Add($"y{i}x{j}", new Cor() 
                    { 
                        Ch = details[j], 
                        Value = priorityScore[details[j]],
                        PosX = j,
                        PosY = i
                    });

                    if(details[j] == 'E')
                    {
                        targetCor = new Cor()
                        {
                            Ch = details[j],
                            Value = priorityScore[details[j]],
                            PosX = j,
                            PosY = i
                        };
                    }

                    if(details[j] == 'S')
                    {
                        startCor = new Cor()
                        {
                            Ch = details[j],
                            Value = priorityScore[details[j]],
                            PosX = j,
                            PosY = i
                        };
                    }
                }
            }

            var route = new RouteCor()
            {
                CurPosX = startCor.PosX,
                CurPosY = startCor.PosY,
                Priority = 0,
                TriedPositions = new Dictionary<string, bool>(),
                Route = new List<Cor>() { startCor }
            };

            while (route.Priority < targetCor.Value)
            {
                var match = new List<Cor>();
                if (coordinates.ContainsKey($"y{route.CurPosY}x{route.CurPosX - 1}")
                    && (coordinates[$"y{route.CurPosY}x{route.CurPosX - 1}"].Value == route.Priority
                    || coordinates[$"y{route.CurPosY}x{route.CurPosX - 1}"].Value == route.Priority + 1))
                {
                    match.Add(coordinates[$"y{route.CurPosY}x{route.CurPosX - 1}"]);
                }
                if (coordinates.ContainsKey($"y{route.CurPosY - 1}x{route.CurPosX}")
                    && (coordinates[$"y{route.CurPosY - 1}x{route.CurPosX}"].Value == route.Priority
                    || coordinates[$"y{route.CurPosY - 1}x{route.CurPosX}"].Value == route.Priority + 1))
                {
                    match.Add(coordinates[$"y{route.CurPosY - 1}x{route.CurPosX}"]);
                }
                if (coordinates.ContainsKey($"y{route.CurPosY +1}x{route.CurPosX}")
                    && (coordinates[$"y{route.CurPosY + 1}x{route.CurPosX}"].Value == route.Priority
                    || coordinates[$"y{route.CurPosY + 1}x{route.CurPosX}"].Value == route.Priority + 1))
                {
                    match.Add(coordinates[$"y{route.CurPosY + 1}x{route.CurPosX}"]);
                }
                
                if (coordinates.ContainsKey($"y{route.CurPosY}x{route.CurPosX+1}")
                    && (coordinates[$"y{route.CurPosY}x{route.CurPosX + 1}"].Value == route.Priority
                    || coordinates[$"y{route.CurPosY}x{route.CurPosX + 1}"].Value == route.Priority + 1))
                {
                    match.Add(coordinates[$"y{route.CurPosY}x{route.CurPosX + 1}"]);
                }

                // Blocked path
                

                var nextCor = match.Where(m => !route.Route.Any(r => r.PosX == m.PosX && r.PosY == m.PosY))
                    .OrderByDescending(s => s.Value)
                    .FirstOrDefault(s => !route.TriedPositions.ContainsKey($"y{s.PosY}x{s.PosX}"));
                
                if(nextCor != null)
                {
                    route.CurPosY = nextCor.PosY;
                    route.CurPosX = nextCor.PosX;
                    route.Priority = nextCor.Value;
                    route.Route.Add(nextCor);
                }
                else
                {
                    var prev = route.Route.TakeLast(1).First();

                    route.Route.Remove(prev);
                    route.CurPosY = prev.PosY;
                    route.CurPosX = prev.PosX;
                    route.Priority = prev.Value;

                    route.TriedPositions.Add($"y{route.CurPosY}x{route.CurPosX}", true);
                }

                Console.WriteLine($"{string.Join("", route.Route.Select(s => s.Ch))}");
            }

            Console.WriteLine($"Result 1: {route.Route.Count}");
        }

        private static void Puzzle2(List<string> input)
        {
            
            Console.WriteLine($"Result 2: ");
        }
        public class Cor
        {
            public int PosX { get; set; }
            public int PosY { get; set; }
            public char Ch { get; set; }
            public int Value { get; set; }
        }
        public class RouteCor 
        {
            public int CurPosX { get; set; }
            public int CurPosY { get; set; }
            public int Priority { get; set; }
            public List<Cor> Route { get; set; }
            public Dictionary<string, bool> TriedPositions { get; set; }
        }
    }
}
