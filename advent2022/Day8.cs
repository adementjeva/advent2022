using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent2022
{
    public static class Day8
    {
        public static void Exec()
        {
            var directory = "C:/Users/Anna/Documents/Advent/advent2022/advent2022/";
            List<string> input = File.ReadAllLines(directory + "/resources/Day8-1.txt").ToList();

            Puzzle1(input);
            Puzzle2(input);
        }

        private static void Puzzle1(List<string> input)
        {
            long visible = 0;
            long invisible = 0;

            var trees = new List<Tree>();
            var rowCountIndex = input.Count -1;
            var colCountIndex = input[0].ToCharArray().Length -1;

            for (int i = 0; i <= rowCountIndex; i++)
            {
                var details = input[i];
                var chList = details.ToCharArray().ToList();
                
                for (int j = 0; j <= colCountIndex; j++)
                {
                    trees.Add(new Tree
                    {
                        Row = i,
                        Col = j,
                        Value = int.Parse(chList[j].ToString()),
                        UniqueId = $"{i}{j}{chList[j]}"
                    });
                }
            }

            foreach (var item in trees)
            {
                if (item.Row == 0 || item.Col == 0 || item.Row == rowCountIndex || item.Col == colCountIndex)
                {
                    visible += 1;
                    continue;
                }

                var matchedTrees = trees.Where(t =>
                    (t.Col == item.Col || t.Row == item.Row)
                    && t.UniqueId != item.UniqueId);

                var topVisible = matchedTrees.Where(t => t.Row < item.Row && t.Col == item.Col)
                    .All(t => t.Value < item.Value);
                var bottomVisible = matchedTrees.Where(t => t.Row > item.Row && t.Col == item.Col)
                    .All(t => t.Value < item.Value);

                var leftVisible = matchedTrees.Where(t => t.Col > item.Col && t.Row == item.Row)
                    .All(t => t.Value < item.Value);
                var rightVisible = matchedTrees.Where(t => t.Col < item.Col && t.Row == item.Row)
                    .All(t => t.Value < item.Value);

                if (topVisible || bottomVisible || leftVisible || rightVisible)
                {
                    visible += 1;
                }
                else
                {
                    invisible += 1;                    
                }
            }
            
            Console.WriteLine($"Result 1: visible - {visible}, invisible - {invisible}");
        }

        public class Tree 
        {
            public bool Invisible { get; set; }
            public int Col { get; set; }
            public int Row { get; set; }
            public int Value { get; set; }
            public string UniqueId { get; set; }
        }

        private static void Puzzle2(List<string> input)
        {
            var trees = new List<Tree>();
            var rowCountIndex = input.Count - 1;
            var colCountIndex = input[0].ToCharArray().Length - 1;

            for (int i = 0; i <= rowCountIndex; i++)
            {
                var details = input[i];
                var chList = details.ToCharArray().ToList();

                for (int j = 0; j <= colCountIndex; j++)
                {
                    trees.Add(new Tree
                    {
                        Row = i,
                        Col = j,
                        Value = int.Parse(chList[j].ToString()),
                        UniqueId = $"{i}{j}{chList[j]}"
                    });
                }
            }
            long highestScore = 0;
            foreach (var item in trees)
            {
                if (item.Row == 0 || item.Col == 0 || item.Row == rowCountIndex || item.Col == colCountIndex)
                {
                    continue;
                }

                var matchedTrees = trees.Where(t =>
                    (t.Col == item.Col || t.Row == item.Row)
                    && t.UniqueId != item.UniqueId);

                var topTree = matchedTrees.Where(t => t.Row < item.Row && t.Col == item.Col)
                    .Last(t => t.Value >= item.Value || t.Row == 0);

                var bottomTree = matchedTrees.Where(t => t.Row > item.Row && t.Col == item.Col)
                    .First(t => t.Value >= item.Value || t.Row == rowCountIndex);

                var leftTree = matchedTrees.Where(t => t.Col < item.Col && t.Row == item.Row)
                    .Last(t => t.Value >= item.Value || t.Col == 0);
                var rightTree = matchedTrees.Where(t => t.Col > item.Col && t.Row == item.Row)
                    .First(t => t.Value >= item.Value || t.Col == colCountIndex);

                var topDistance = item.Row - topTree.Row;
                var bottomDistance = bottomTree.Row - item.Row;
                var leftDistance = item.Col - leftTree.Col;
                var rightDistance = rightTree.Col - item.Col;

                long sum = topDistance * bottomDistance * leftDistance * rightDistance;

                if (sum > highestScore)
                {
                    highestScore = sum;
                }
            }
            Console.WriteLine($"Result 2: {highestScore}");
        }
    }
}
