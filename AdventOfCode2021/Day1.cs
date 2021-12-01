using MoreLinq;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day1 : ISolver
    {
        public (string, string) ExpectedResult => ("1548", "");

        public (string, string) Solve(string[] input)
        {
            return ($"{Solve(input,1)}", $"");
        }

        long Solve(IEnumerable<string> input,int _) =>
            input.Select(long.Parse)
                .Pairwise((a, b) => b - a)
                .Count(a => a > 0);
    }
}
