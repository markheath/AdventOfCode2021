using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{

    public class Day8 : ISolver
    {
        public (string, string) ExpectedResult => ("543", "");

        public (string, string) Solve(string[] input)
        {
            var part1 = input.Select(ParseLine)
                .Sum(l => CountEasy(l.outputs));

            return ($"{part1}", $"");
        }

        // 0 has 6 segments
        // 1 has 2 segments = unique
        // 2 has 5 segments
        // 3 has 5 segments
        // 4 has 4 segments - unique
        // 5 has 5 segments
        // 6 has 6 segments
        // 7 has 3 segments - unqiue
        // 8 has 7 segments - unique
        // 9 has 6 segments
        private static readonly HashSet<int> easyLengths = new HashSet<int>() { 2, 4, 3, 7 };

        public (string[] patterns, string[] outputs) ParseLine(string line)
        {
            var parts = line.Split('|');
            var patterns = parts[0].Trim().Split(' ').ToArray();
            var outputs = parts[1].Trim().Split(' ').ToArray();
            return (patterns, outputs);
        }

        public int CountEasy(string[] outputs)
        {
            return outputs.Count(o => easyLengths.Contains(o.Length));
        }

    }

}
