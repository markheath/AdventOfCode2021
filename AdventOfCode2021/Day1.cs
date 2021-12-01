using MoreLinq;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day1 : ISolver
    {
        public (string, string) ExpectedResult => ("1548", "1589");

        public (string, string) Solve(string[] input)
        {
            return ($"{Part1(input)}", $"{Part2(input)}");
        }

        long Part1(IEnumerable<string> input) =>
            input.Select(long.Parse)
                .Pairwise((a, b) => b - a)
                .Count(a => a > 0);

        long Part2(IEnumerable<string> input) =>
            input.Select(long.Parse)
                .Window(3)
                .Select(w => w.Sum())
                .Pairwise((a, b) => b - a)
                .Count(a => a > 0);

    }
}
