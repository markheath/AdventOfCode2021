using MoreLinq;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day2 : ISolver
    {
        public (string, string) ExpectedResult => ("1484118", "1463827010");

        public (string, string) Solve(string[] input)
        {
            return ($"{Part1(input)}", $"{Part2(input)}");
        }

        private Dictionary<string, (long, long)> vectors = new Dictionary<string, (long, long)>()
        {
            { "forward", (1,0) },
            { "down", (0,1) },
            { "up", (0,-1) }
        };

        long Part1(IEnumerable<string> input) =>
            input.Select(s => s.Split(' '))
                .Select(p => new { Dir = vectors[p[0]], Dist = long.Parse(p[1]) })
                .Scan((0L, 0L), (cur, inst) => (cur.Item1 + inst.Dist * inst.Dir.Item1, cur.Item2 + inst.Dist * inst.Dir.Item2))
                .Select(x => x.Item1 * x.Item2)
                .Last();

        long Part2(IEnumerable<string> input) =>
            input.Select(s => s.Split(' '))
                .Select(p => new { Dir = vectors[p[0]], Dist = long.Parse(p[1]) })
                // h, d, a
                .Scan((0L, 0L, 0L), (cur, inst) => (cur.Item1 + inst.Dist * inst.Dir.Item1, cur.Item2 + inst.Dist * inst.Dir.Item1 * cur.Item3, cur.Item3 + inst.Dist * inst.Dir.Item2))
                //.Pipe(x => System.Console.WriteLine(x))
                .Select(x => x.Item1 * x.Item2)
                .Last();

    }
}
