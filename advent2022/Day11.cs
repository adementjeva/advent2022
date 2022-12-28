using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent2022
{
    public static class Day11
    {
        public static void Exec()
        {
            var directory = "C:/Users/Anna/Documents/Advent/advent2022/advent2022/";
            List<string> input = File.ReadAllLines(directory + "/resources/Day11-1.txt").ToList();

            var monekies = new List<Monkey>();
            for (int i = 0; i < input.Count; i += 7)
            {
                var details1 = input[i].Split(" ", StringSplitOptions.RemoveEmptyEntries); //Monkey 0:
                var details2 = input[i + 1].Split(":", StringSplitOptions.RemoveEmptyEntries); //Starting items: 79, 98
                var details3 = input[i + 2].Split(":", StringSplitOptions.RemoveEmptyEntries); //Operation: new = old * 19
                var details4 = input[i + 3].Split(" ", StringSplitOptions.RemoveEmptyEntries); //Test: divisible by 23
                var details5 = input[i + 4].Split(" ", StringSplitOptions.RemoveEmptyEntries); //If true: throw to monkey 2
                var details6 = input[i + 5].Split(" ", StringSplitOptions.RemoveEmptyEntries); //If false: throw to monkey 3

                var monkey = new Monkey()
                {
                    Id = int.Parse(details1[1].Replace(":", "")),
                    Items = new List<long>(),
                    Operation = details3[1],
                    TestDivision = int.Parse(details4[3]),
                    IfTrueMonkeyId = int.Parse(details5[5]),
                    IfFalseMonkeyId = int.Parse(details6[5])
                };

                if(details2.Length == 2)
                {
                monkey.Items = details2[1].Split(",", StringSplitOptions.RemoveEmptyEntries).Select(s => long.Parse(s)).ToList();
                }
            monekies.Add(monkey);
            }

           // Puzzle1(monekies);
            Puzzle2(monekies);
        }

        private static void Puzzle1(List<Monkey> monkies)
        {
            var rounds = 0;

            while (rounds < 20)
            {
                var mS = monkies.Select(s => s).ToList();
                for (int i = 0; i < monkies.Count; i++)
                {
                    for (int j = 0; j < monkies[i].Items.Count; j++)
                    {
                        monkies[i].ItemCounter += 1;
                        var newVal = Calculate(monkies[i].Operation, monkies[i].Items[j]);
                        newVal = (long)Math.Round((decimal)newVal / 3, MidpointRounding.ToZero);

                        if (newVal % monkies[i].TestDivision == 0)
                        {
                            var match = monkies.Find(m => m.Id == monkies[i].IfTrueMonkeyId);
                            match.Items.Add(newVal);
                        }
                        else
                        {
                            var match = monkies.Find(m => m.Id == monkies[i].IfFalseMonkeyId);
                            match.Items.Add(newVal);
                        }
                    }
                    monkies[i].Items = new List<long>();
                }

                foreach (var item in monkies)
                {
                    Console.WriteLine($"{item.Id} {string.Join(",", item.Items)}");
                }
                rounds++;

            }

            foreach (var item in monkies)
            {
                Console.WriteLine($"{item.Id} {item.ItemCounter}");
            }
            Console.WriteLine($"Result 1: ");
        }

        private static void Puzzle2(List<Monkey> monkies)
        {
            var rounds = 1;

            while (rounds <= 20)
            {
                for (int i = 0; i < monkies.Count; i++)
                {
                    for (int j = 0; j < monkies[i].Items.Count; j++)
                    {
                        monkies[i].ItemCounter += 1;
                        var newVal = Calculate(monkies[i].Operation, monkies[i].Items[j]);

                        if (newVal % monkies[i].TestDivision == 0)
                        {
                            var match = monkies.Find(m => m.Id == monkies[i].IfTrueMonkeyId);
                            match.Items.Add(newVal);
                        }
                        else
                        {
                            var match = monkies.Find(m => m.Id == monkies[i].IfFalseMonkeyId);
                            match.Items.Add(newVal);
                        }
                    }
                    monkies[i].Items = new List<long>();
                }

                Console.WriteLine($"Round: {rounds} ");
                foreach (var item in monkies)
                {
                    Console.Write($" Monkey {item.Id} {item.ItemCounter}");
                }
                Console.WriteLine("");

                rounds++;

            }

            

            //Console.WriteLine($"Result 2: ");
        }


        private static long Calculate(string expression, long value)
        {
            var details = expression.Replace("old", value.ToString()).Split("=", StringSplitOptions.RemoveEmptyEntries);
            var equasionDetails = details[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

            switch (equasionDetails[1])
            {
                case "*":
                    return long.Parse(equasionDetails[0]) * long.Parse(equasionDetails[2]);
                case "/":
                    return long.Parse(equasionDetails[0]) / long.Parse(equasionDetails[2]);
                case "+":
                    return long.Parse(equasionDetails[0]) + long.Parse(equasionDetails[2]);
                case "-":
                    return long.Parse(equasionDetails[0]) - long.Parse(equasionDetails[2]);
            }
            return 0;
        }

        public class Monkey
        {
            public int Id { get; set; }
            public List<long> Items { get; set; }
            public string Operation { get; set; }
            public int TestDivision { get; set; }
            public int IfFalseMonkeyId { get; set; }
            public int IfTrueMonkeyId { get; set; }
            public int ItemCounter { get; set; }
        }
    }
}
