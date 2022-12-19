using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent2022
{
    public static class Day7
    {
        public static void Exec()
        {
            var directory = "C:/Users/Anna/Documents/Advent/advent2022/advent2022/";
            List<string> input = File.ReadAllLines(directory + "/resources/Day7-1.txt").ToList();

            Puzzle1(input);
            //Puzzle2(input);
        }

        private static void Puzzle1(List<string> input)
        {
            var count = 0;
            long size = 0;
            var listDir = new List<Dir>();
            var parents = new List<string>();

            for (int i = 0; i < input.Count(); i++)
            {
                var details = input[i].Split(" ");
                var line = input[i];
                var parentRoute = string.Join("", parents);
                if (line.StartsWith("$ ls"))
                {
                    continue;
                } 
                else if (line.StartsWith("dir"))
                {
                    listDir.Add(new Dir
                    {
                        Name = $"{parentRoute}{details[1]}",
                        File = false,
                        Parent = parentRoute
                    });
                }
                else if (line.StartsWith("$ cd") && !line.EndsWith(".."))
                {
                    parents.Add(details[2]);
                }
                else if (line.StartsWith("$ cd .."))
                {
                    parents.RemoveAt(parents.Count() -1);
                }
                else
                {
                    listDir.Add(new Dir
                    {
                        Name = $"{parentRoute}{details[1]}",
                        File = true,
                        Parent = parentRoute,
                        Size = long.Parse(details[0])
                    });
                }
            }

            var files = listDir.Where(s => s.File);

            foreach (var file in files)
            {
                AssigneSize(listDir, file);
            }

            foreach (var item in listDir.Where(s => s.Size <= 100000 && !s.File))
            {
                size += item.Size;
                Console.WriteLine($"Result 1: {item.Size}");
            }
            Console.WriteLine($"Result 1: {size}");
        }

        private static void AssigneSize(List<Dir> listDir, Dir file)
        {
            var match = listDir.FirstOrDefault(s => s.Name == file.Parent && !s.File);
            if (match == null)
                return;

            match.Size += file.Size;
            AssigneSize(listDir, match);
        }

        private static void Puzzle2(List<string> input)
        {
            Console.WriteLine("Result 2: ");
        }
    }

    public class Dir
    {
        public long Size { get; set; }
        public string Name { get; set; }
        public bool File { get; set; }
        public string Parent { get; set; }
    }
}
