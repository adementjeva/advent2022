using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent2022
{
    public static class Day6
    {
        public static void Exec()
        {
            var directory = "C:/Users/Anna/Documents/Advent/advent2022/advent2022/";
            List<string> input = File.ReadAllLines(directory + "/resources/Day6-1.txt").ToList();

            //Puzzle1(input);
            Puzzle2(input);
        }

        private static void Puzzle1(List<string> input)
        {
            foreach (var item in input)
            {
                var chList = item.ToCharArray();
                var marker = 0;
                Console.WriteLine("Input: " + item);
                while (marker < chList.Length -3 )
                {
                    var curCh = chList[marker];
                    var next1Ch = chList[marker + 1];
                    var next2Ch = chList[marker + 2];
                    var next3Ch = chList[marker + 3];
                    Console.WriteLine($"{curCh}{next1Ch}{next2Ch}{next3Ch}");
                    if(next3Ch == next2Ch || next3Ch == next1Ch || next3Ch == curCh)
                    {
                        marker += 1;
                    }
                    else if (next2Ch == next1Ch || next2Ch == curCh)
                    {
                        marker += 1;
                    }
                    else if (next1Ch == curCh)
                    {
                        marker += 1;
                    }
                    else
                    {
                        Console.WriteLine($"Marker: {marker + 4}");
                        break;
                    }
                }

            }
            Console.WriteLine("Result 1: " );
        }


        private static void Puzzle2(List<string> input)
        {
            foreach (var item in input)
            {
                var itemIndex = 0;
                var marker = 13;

                while (itemIndex < item.Length)
                {
                    var subItem = item.Substring(itemIndex, 14);
                    Console.WriteLine("Input: " + subItem);

                    var chList = subItem.ToCharArray().ToList();
                    var matched = false;
                    while (chList.Any())
                    {
                        var firstItem = chList[0];
                        chList = chList.Skip(1).ToList();
                        if (chList.Contains(firstItem))
                        {
                            matched = true;
                        }
                    }

                    if (!matched)
                        break;
                    itemIndex += 1;
                }
                Console.WriteLine($"Marker: {marker + itemIndex + 1}");

            }
            Console.WriteLine("Result 2: ");
        }
    }
}
