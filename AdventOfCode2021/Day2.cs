using MoreLinq;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day2 : ISolver
    {
        public (string, string) ExpectedResult => ("1484118", "");

        public (string, string) Solve(string[] input)
        {
            return ($"{Part1(input)}", $"{Part2(input)}");
        }

        private Dictionary<string, (int, int)> vectors = new Dictionary<string, (int, int)>()
        {
            { "forward", (1,0) },
            { "down", (0,1) },
            { "up", (0,-1) }
        };

        long Part1(IEnumerable<string> input) =>
            input.Select(s => s.Split(' '))
                .Select(p => new { Dir = vectors[p[0]], Dist = int.Parse(p[1]) })
                .Scan((0, 0), (cur, inst) => (cur.Item1 + inst.Dist * inst.Dir.Item1, cur.Item2 + inst.Dist * inst.Dir.Item2))
                .Select(x => x.Item1 * x.Item2)
                .Last();

        long Part2(IEnumerable<string> input) =>
            -1;

    }
}
