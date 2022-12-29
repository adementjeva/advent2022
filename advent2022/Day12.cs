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

                    if (details[j] == 'E')
                    {
                        targetCor = new Cor()
                        {
                            Ch = details[j],
                            Value = priorityScore[details[j]],
                            PosX = j,
                            PosY = i
                        };
                    }

                    if (details[j] == 'S')
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

            var routes = new List<RouteCor>()
            {
                new RouteCor
                {
                    Route = new List<Cor>(),
                    CurPositions = startCor,
                    Priority = 0
                }
            };

            var found = false;

            while (!found)
            {
                var routeCount = routes.Count;

                for (int i = 0; i < routeCount; i++)
                {
                    routes[i].Route.Add(routes[i].CurPositions);

                    var match = new List<Cor>();

                    if (coordinates.ContainsKey($"y{routes[i].CurPositions.PosY + 1}x{routes[i].CurPositions.PosX}")
                    && (coordinates[$"y{routes[i].CurPositions.PosY + 1}x{routes[i].CurPositions.PosX}"].Value == routes[i].Priority
                    || coordinates[$"y{routes[i].CurPositions.PosY + 1}x{routes[i].CurPositions.PosX}"].Value == routes[i].Priority + 1))
                    {
                        match.Add(coordinates[$"y{routes[i].CurPositions.PosY + 1}x{routes[i].CurPositions.PosX}"]);
                    }

                    if (coordinates.ContainsKey($"y{routes[i].CurPositions.PosY - 1}x{routes[i].CurPositions.PosX}")
                    && (coordinates[$"y{routes[i].CurPositions.PosY - 1}x{routes[i].CurPositions.PosX}"].Value == routes[i].Priority
                    || coordinates[$"y{routes[i].CurPositions.PosY - 1}x{routes[i].CurPositions.PosX}"].Value == routes[i].Priority + 1))
                    {
                        match.Add(coordinates[$"y{routes[i].CurPositions.PosY - 1}x{routes[i].CurPositions.PosX}"]);
                    }

                    if (coordinates.ContainsKey($"y{routes[i].CurPositions.PosY}x{routes[i].CurPositions.PosX + 1}")
                    && (coordinates[$"y{routes[i].CurPositions.PosY}x{routes[i].CurPositions.PosX + 1}"].Value == routes[i].Priority
                    || coordinates[$"y{routes[i].CurPositions.PosY}x{routes[i].CurPositions.PosX + 1}"].Value == routes[i].Priority + 1))
                    {
                        match.Add(coordinates[$"y{routes[i].CurPositions.PosY}x{routes[i].CurPositions.PosX + 1}"]);
                    }

                    if (coordinates.ContainsKey($"y{routes[i].CurPositions.PosY}x{routes[i].CurPositions.PosX - 1}")
                    && (coordinates[$"y{routes[i].CurPositions.PosY}x{routes[i].CurPositions.PosX - 1}"].Value == routes[i].Priority
                    || coordinates[$"y{routes[i].CurPositions.PosY}x{routes[i].CurPositions.PosX - 1}"].Value == routes[i].Priority + 1))
                    {
                        match.Add(coordinates[$"y{routes[i].CurPositions.PosY}x{routes[i].CurPositions.PosX - 1}"]);
                    }

                    var potentialMatch = match.Where(s => !routes[i].Route.Any(r => r.PosX == s.PosX && r.PosY == s.PosY)).ToList();

                    if (potentialMatch.Any())
                    {
                        foreach (var item in potentialMatch)
                        {
                            var newBranch = new RouteCor
                            {
                                CurPositions = item,
                                Priority = item.Value,
                                Route = routes[i].Route.ToList()
                            };

                            routes.Add(newBranch);

                            if (item.Value == 27)
                                found = true;

                            Console.WriteLine($"{string.Join("", newBranch.Route.Select(s => s.Ch))}");
                        }

                        if (potentialMatch.First().Value == 27)
                            found = true;

                    }

                    routes.RemoveAt(i);
                }

            }
            var endRoute = routes.Find(s => s.Priority == 27);

            Console.WriteLine($"Result 1: {endRoute.Route.Count}");
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
            public Cor CurPositions { get; set; }
            public int Priority { get; set; }
            public List<Cor> Route { get; set; }
        }
    }
}
