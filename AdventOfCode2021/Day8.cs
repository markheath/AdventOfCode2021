using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{

    public class Day8 : ISolver
    {
        public (string, string) ExpectedResult => ("543", "994266");

        public (string, string) Solve(string[] input)
        {
            var lines = input.Select(ParseLine).ToList();
            var part1 = lines.Sum(l => CountEasy(l.outputs));
            var part2 = lines.Sum(l => GetOutput(l.patterns, l.outputs));
            

            return ($"{part1}", $"{part2}");
        }

        // 1 has 2 segments = unique
        // 4 has 4 segments - unique
        // 7 has 3 segments - unqiue
        // 8 has 7 segments - unique
        private static readonly HashSet<int> easyLengths = new HashSet<int>() { 2, 4, 3, 7 };

        public (string[] patterns, string[] outputs) ParseLine(string line)
        {
            var parts = line.Split('|');
            var patterns = parts[0].Trim().Split(' ').Select(s => string.Concat(s.OrderBy(c => c))).ToArray();
            var outputs = parts[1].Trim().Split(' ').Select(s => string.Concat(s.OrderBy(c => c))).ToArray();
            return (patterns, outputs);
        }

        public int CountEasy(string[] outputs)
        {
            return outputs.Count(o => easyLengths.Contains(o.Length));
        }


        public int GetOutput(string[] patterns, string[] outputs)
        {
            var lookup = CreateLookup(patterns);
            return int.Parse(string.Concat(outputs.Select(o => lookup[o].ToString())));
        }

        public Dictionary<string,int> CreateLookup(string[] patterns)
        {
            // TOP occurs 8 times
            // MIDDLE occurs 7 times
            // BOTTOM occurs 7 times
            // TOP-LEFT OCCURS 6 times
            // TOP-RIGHT occurs 8 times
            // BOTTOM-LEFT occurs 4 times
            // BOTTOM-RIGHT occurs 9 times 

            


            var one = patterns.Single(p => p.Length == 2);
            var four = patterns.Single(p => p.Length == 4);
            var seven = patterns.Single(p => p.Length == 3);
            var eight = patterns.Single(p => p.Length == 7);

            var segmentCounts = string.Concat(patterns).CountBy(c => c).ToList();
            
            var bottomRight = segmentCounts.Single(kvp => kvp.Value == 9).Key; // BOTTOM-RIGHT is the one that occurs 9 times
            var bottomLeft = segmentCounts.Single(kvp => kvp.Value == 4).Key; // BOTTOM-LEFT is the one that occurs 4 times
            var topLeft = segmentCounts.Single(kvp => kvp.Value == 6).Key; // TOP-LEFT is the one that occurs 6 times

            var top = seven.Except(one).Single(); // compare 1 and seven to find out what TOP is
            var topRight = segmentCounts.Single(kvp => kvp.Key != top && kvp.Value == 8).Key; // TOP-RIGHT is the the other one that occurs 8 times excluding TOP

            var middle = four.Single(c => c != topLeft && c != topRight && c != bottomRight); // MIDDLE is in 4
                                                                                              // BOTTOM is the last one


            // 2 has 5 segments
            // 3 has 5 segments
            // 5 has 5 segments
            var fiveSegments = patterns.Where(p => p.Length == 5).ToArray();
            // the three horizontals are are common to 2,3,5
            // 2 and 3 share TOP-RIGHT
            // 3 and 5 share BOTTOM-RIGHT
            var two = fiveSegments.Single(p => p.Contains(bottomLeft)); // BOTTOM-LEFT is unique to 2
            var three = fiveSegments.Single(p => p.Contains(topRight) && p.Contains(bottomRight));
            var five = fiveSegments.Single(p => p.Contains(topLeft) && p.Contains(bottomRight));

            // 0 has 6 segments
            // 6 has 6 segments
            // 9 has 6 segments
            var sixSegments = patterns.Where(p => p.Length == 6).ToArray();
            // abgf and common to 0,6,9
            var zero = sixSegments.Single(p => !p.Contains(middle));
            var six = sixSegments.Single(p => !p.Contains(topRight));
            var nine = sixSegments.Single(p => !p.Contains(bottomLeft));


            return new Dictionary<string, int>() {  {zero, 0 }, { one, 1 }, { two, 2 },
                {three, 3}, { four, 4 }, {five, 5 }, {six, 6 }, { seven, 7 }, { eight, 8 }, {nine, 9 } };

        }

    }

}
