using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent2022
{
    //Lowercase item types a through z have priorities 1 through 26.
    //Uppercase item types A through Z have priorities 27 through 52.
    public static class Day3
    {
        public static string azAZ = "a b c d e f g h i j k l m n o p q r s t u v w x y z A B C D E F G H I J K L M N O P Q R S T U V W X Y Z";
        public static Dictionary<string, int> priorityScore;
        public static void Exec()
        {
            var directory = "C:/Users/Anna/Documents/Advent/advent2022/advent2022/";
            List<string> input = File.ReadAllLines(directory + "/resources/Day3-1.txt").ToList();

            priorityScore = new Dictionary<string, int>();
            var letters = azAZ.Split(" ");

            for (int i = 1; i <= letters.Length; i++)
            {
                priorityScore.Add(letters[i - 1], i);
            }

            Puzzle1(input);
            Puzzle2(input);
        }

        private static void Puzzle1(List<string> input)
        {
            var sum = 0;

            foreach (var item in input)
            {
                var ch = item.ToCharArray();
                var len = ch.Length / 2;
                var start = ch.Take(len);
                var end = ch.TakeLast(len);

                foreach (var stLet in start)
                {
                    if (end.Contains(stLet))
                    {
                        sum += priorityScore[stLet.ToString()];
                        break;
                    }
                }
            }

            Console.WriteLine("Result 1: " + sum);
        }


        private static void Puzzle2(List<string> input)
        {
            var sum = 0;
            var groupCount = 1;

            for (int i = 0; i < input.Count(); i++)
            {
                if (groupCount == 4) groupCount = 1;

                if (groupCount == 2)
                {
                    var curRow = input[i].ToCharArray();
                    var prevRow = input[i -1].ToCharArray();
                    var nextRow = input[i + 1].ToCharArray();

                    foreach (var ch in curRow)
                    {
                        if(prevRow.Contains(ch) && nextRow.Contains(ch))
                        {
                            sum += priorityScore[ch.ToString()];
                            break;
                        }
                    }
                }

                groupCount += 1;
            }

            Console.WriteLine("Result 2: " + sum);
        }
    }
}
