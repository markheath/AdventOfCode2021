using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{

    public class Day3 : ISolver
    {
        public (string, string) ExpectedResult => ("3813416", "2990784");
        private List<(int, int)> counts;

        public (string, string) Solve(string[] input)
        {
            counts = GenerateBitCounts(input, input[0].Length);

            return ($"{Part1(input)}", $"{Part2(input)}");
        }


        long Part1(IEnumerable<string> input)
        {            
            var gamma = new string(counts.Select(c => c.Item1 > c.Item2 ? '1' : '0').ToArray());
            var epsilon = new string(counts.Select(c => c.Item1 > c.Item2 ? '0' : '1').ToArray());
            Console.WriteLine($"Gamma {gamma}, epsilon{epsilon}");
            return Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);
        }

        private static List<(int, int)> GenerateBitCounts(IEnumerable<string> input, int len)
        {
            var counts = Enumerable.Range(0, len)
                .Select(n => (0, 0))
                .ToList();
            foreach (var number in input)
                for (var n = 0; n < len; n++)
                    counts[n] = number[n] == '1' ? (counts[n].Item1, counts[n].Item2 + 1) : (counts[n].Item1 + 1, counts[n].Item2);
            return counts;
        }


        long Part2(IEnumerable<string> input)
        {
            string oxygenGenerator = null;
            var items = input.ToList();
            int pos = 0;
            while (oxygenGenerator == null)
            {
                // if they are equal the most popular bit should be 1
                var mostPopularBit = counts[pos].Item1 > counts[pos].Item2 ? '0' : '1';
                items.RemoveAll(c => c[pos] != mostPopularBit);
                if (items.Count == 1) oxygenGenerator = items[0];
                pos++;
                counts = GenerateBitCounts(items, items[0].Length);
            }

            string co2Scrubber = null;
            items = input.ToList();
            counts = GenerateBitCounts(items, items[0].Length);
            pos = 0;
            while (co2Scrubber == null)
            {
                // if they are equal, least common bit should be 0
                var leastCommonBit = counts[pos].Item1 > counts[pos].Item2 ? '1' : '0';
                items.RemoveAll(c => c[pos] != leastCommonBit);
                if (items.Count == 1) co2Scrubber = items[0];
                pos++;
                counts = GenerateBitCounts(items, items[0].Length);
            }
            Console.WriteLine($"oxygenGenerator {oxygenGenerator}, co2Scrubber {co2Scrubber}");
            return Convert.ToInt64(oxygenGenerator, 2) * Convert.ToInt64(co2Scrubber, 2);

        }


}
}
