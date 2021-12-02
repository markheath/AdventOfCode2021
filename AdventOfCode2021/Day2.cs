using MoreLinq;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
    public record SubState(long Horizontal, long Depth, long Aim);

    public class Day2 : ISolver
    {
        public (string, string) ExpectedResult => ("1484118", "1463827010");

        public (string, string) Solve(string[] input)
        {
            return ($"{Part1(input)}", $"{Part2(input)}");
        }



        private Dictionary<string, SubState> vectors = new Dictionary<string, SubState>()
        {
            { "forward", new(1,0,0) },
            { "down", new(0,1,0) },
            { "up", new(0,-1,0) }
        };

        long Part1(IEnumerable<string> input) =>
            input.Select(s => s.Split(' '))
                .Select(p => new { Dir = vectors[p[0]], Dist = long.Parse(p[1]) })
                .Scan(new SubState(0,0,0), (cur, inst) => new (cur.Horizontal + inst.Dist * inst.Dir.Horizontal, 
                    cur.Depth + inst.Dist * inst.Dir.Depth, 0))
                .Select(x => x.Horizontal * x.Depth)
                .Last();

        long Part2(IEnumerable<string> input) =>
            input.Select(s => s.Split(' '))
                .Select(p => new { Dir = vectors[p[0]], Dist = long.Parse(p[1]) })
                // h, d, a
                .Scan(new SubState(0, 0, 0), (cur, inst) => new(cur.Horizontal + inst.Dist * inst.Dir.Horizontal, cur.Depth + inst.Dist * inst.Dir.Horizontal * cur.Aim, cur.Aim + inst.Dist * inst.Dir.Depth))
                //.Pipe(x => System.Console.WriteLine(x))
                .Select(x => x.Horizontal * x.Depth)
                .Last();

    }
}
