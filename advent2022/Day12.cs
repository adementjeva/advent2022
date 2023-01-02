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

            //Puzzle1(input);
            Puzzle2(input);
        }

        private static void Puzzle1(List<string> input)
        {
            var coordinates = new Dictionary<string, Cor>();
            var targetCor = new Cor();
            var startCor = new Cor();

            // map coordinates
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

            var endRoute = MapRoute(startCor, coordinates);
        }

        private static RouteCor MapRoute(Cor startCor, Dictionary<string, Cor> coordinates)
        {
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
            var visitedPosition = new Dictionary<string, Cor>();

            while (!found)
            {
                var routeCount = routes.Count;
                var branchRoutes = new List<RouteCor>();

                if (routeCount == 0)
                    found = true;

                for (int i = 0; i < routeCount; i++)
                {
                    var match = new List<Cor>();

                    if (coordinates.ContainsKey($"y{routes[i].CurPositions.PosY + 1}x{routes[i].CurPositions.PosX}")
                    && !coordinates[$"y{routes[i].CurPositions.PosY + 1}x{routes[i].CurPositions.PosX}"].Blocked
                    && (coordinates[$"y{routes[i].CurPositions.PosY + 1}x{routes[i].CurPositions.PosX}"].Value <= routes[i].Priority
                    || coordinates[$"y{routes[i].CurPositions.PosY + 1}x{routes[i].CurPositions.PosX}"].Value == routes[i].Priority + 1))
                    {
                        match.Add(coordinates[$"y{routes[i].CurPositions.PosY + 1}x{routes[i].CurPositions.PosX}"]);
                        coordinates[$"y{routes[i].CurPositions.PosY + 1}x{routes[i].CurPositions.PosX}"].Blocked = true;
                    }

                    if (coordinates.ContainsKey($"y{routes[i].CurPositions.PosY - 1}x{routes[i].CurPositions.PosX}")
                    && !coordinates[$"y{routes[i].CurPositions.PosY - 1}x{routes[i].CurPositions.PosX}"].Blocked
                    && (coordinates[$"y{routes[i].CurPositions.PosY - 1}x{routes[i].CurPositions.PosX}"].Value <= routes[i].Priority
                    || coordinates[$"y{routes[i].CurPositions.PosY - 1}x{routes[i].CurPositions.PosX}"].Value == routes[i].Priority + 1))
                    {
                        match.Add(coordinates[$"y{routes[i].CurPositions.PosY - 1}x{routes[i].CurPositions.PosX}"]);
                        coordinates[$"y{routes[i].CurPositions.PosY - 1}x{routes[i].CurPositions.PosX}"].Blocked = true;
                    }

                    if (coordinates.ContainsKey($"y{routes[i].CurPositions.PosY}x{routes[i].CurPositions.PosX + 1}")
                    && !coordinates[$"y{routes[i].CurPositions.PosY}x{routes[i].CurPositions.PosX + 1}"].Blocked
                    && (coordinates[$"y{routes[i].CurPositions.PosY}x{routes[i].CurPositions.PosX + 1}"].Value <= routes[i].Priority
                    || coordinates[$"y{routes[i].CurPositions.PosY}x{routes[i].CurPositions.PosX + 1}"].Value == routes[i].Priority + 1))
                    {
                        match.Add(coordinates[$"y{routes[i].CurPositions.PosY}x{routes[i].CurPositions.PosX + 1}"]);
                        coordinates[$"y{routes[i].CurPositions.PosY}x{routes[i].CurPositions.PosX + 1}"].Blocked = true;
                    }

                    if (coordinates.ContainsKey($"y{routes[i].CurPositions.PosY}x{routes[i].CurPositions.PosX - 1}")
                    && !coordinates[$"y{routes[i].CurPositions.PosY}x{routes[i].CurPositions.PosX - 1}"].Blocked
                    && (coordinates[$"y{routes[i].CurPositions.PosY}x{routes[i].CurPositions.PosX - 1}"].Value <= routes[i].Priority
                    || coordinates[$"y{routes[i].CurPositions.PosY}x{routes[i].CurPositions.PosX - 1}"].Value == routes[i].Priority + 1))
                    {
                        match.Add(coordinates[$"y{routes[i].CurPositions.PosY}x{routes[i].CurPositions.PosX - 1}"]);
                        coordinates[$"y{routes[i].CurPositions.PosY}x{routes[i].CurPositions.PosX - 1}"].Blocked = true;
                    }

                    var potentialMatch = match
                        .Where(s => !routes[i].Route.Any(r => r.PosX == s.PosX && r.PosY == s.PosY)
                            && !visitedPosition.ContainsKey($"y{s.PosY}x{s.PosX}")).ToList();

                    if (potentialMatch.Any())
                    {
                        foreach (var item in potentialMatch)
                        {
                            var newRoute = routes[i].Route.ToList();
                            newRoute.Add(routes[i].CurPositions);

                            var newBranch = new RouteCor
                            {
                                CurPositions = item,
                                Priority = item.Value,
                                Route = newRoute
                            };

                            branchRoutes.Add(newBranch);

                            if (item.Value == 27)
                                found = true;

                            Console.WriteLine($"{string.Join("", newRoute.Select(s => s.Ch))}");
                        }
                    }
                    if (visitedPosition.ContainsKey($"y{routes[i].CurPositions.PosY}x{routes[i].CurPositions.PosX}"))
                        visitedPosition.Add($"y{routes[i].CurPositions.PosY}x{routes[i].CurPositions.PosX}", routes[i].CurPositions);

                }
                routes = branchRoutes;
            }
            var endRoute = routes.Find(s => s.Priority == 27);

            return endRoute;
        }

        private static void Puzzle2(List<string> input)
        {
            var targetCor = new Cor();
            var startCords = new List<Cor>();

            // map coordinates
            for (int i = 0; i < input.Count; i++)
            {
                var details = input[i].ToCharArray();

                for (int j = 0; j < details.Length; j++)
                {
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

                    if (details[j] == 'S' || details[j] == 'a')
                    {
                        startCords.Add(new Cor()
                        {
                            Ch = details[j],
                            Value = priorityScore[details[j]],
                            PosX = j,
                            PosY = i
                        });
                    }
                }
            }

            var routes = new List<RouteCor>();

            foreach (var sCor in startCords)
            {
                var coordinates = new Dictionary<string, Cor>();
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
                    }
                }
                routes.Add(MapRoute(sCor, coordinates));
            }

            var shrotedRoute = routes.Where(r => r != null).OrderBy(s => s.Route.Count).FirstOrDefault();
            Console.WriteLine($"{string.Join("", shrotedRoute.Route.Select(s => s.Ch))}");
            Console.WriteLine($"Resoult 2 - {shrotedRoute.Route.Count -1}");

        }

        public class Cor
        {
            public int PosX { get; set; }
            public int PosY { get; set; }
            public char Ch { get; set; }
            public int Value { get; set; }
            public bool Blocked { get; set; }
        }
        public class RouteCor 
        {
            public bool Blocked { get; set; }
            public Cor CurPositions { get; set; }
            public int Priority { get; set; }
            public List<Cor> Route { get; set; }
        }
    }
}
