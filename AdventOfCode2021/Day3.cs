using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
    
    public class Day3 : ISolver
    {
        public (string, string) ExpectedResult => ("3813416", "");

        public (string, string) Solve(string[] input)
        {
            return ($"{Part1(input, input[0].Length)}", $"{Part2(input)}");
        }


        long Part1(IEnumerable<string> input, int len)
        {
            var counts = Enumerable.Range(0, len)
                .Select(n => (0, 0))
                .ToList();
            foreach (var number in input)
                for (var n = 0; n < len; n++)
                    counts[n] = number[n] == '1' ? (counts[n].Item1, counts[n].Item2 + 1) : (counts[n].Item1 + 1, counts[n].Item2);

            var gamma = new string(counts.Select(c => c.Item1 > c.Item2 ? '1' : '0').ToArray());
            var epsilon = new string(counts.Select(c => c.Item1 > c.Item2 ? '0' : '1').ToArray());
            Console.WriteLine($"Gamma {gamma}, epsilon{epsilon}");
            return Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);
        }
            

        long Part2(IEnumerable<string> input) =>
            -1;

    }
}
