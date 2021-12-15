using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{

    public class Day15 : ISolver
    {
        public (string, string) ExpectedResult => ("", ""); // 767 is still too high!

        public (string, string) Solve(string[] input)
        {
            var grid = Grid<int>.ParseToGrid(input);
            var best = FindPaths((0, 0), 0, grid).Min();
            return ($"{best}", $"");
        }

        // initially just go right and left so we don't need to remember, HashSet<Coord> visited
        private int bestRisk = int.MaxValue;

        // depth first search
        private IEnumerable<int> FindPaths(Coord pos, int riskSoFar, Grid<int> g)
        {
            Coord target = (g.Width - 1, g.Height - 1);
            var toTry = new List<(Coord, int)>();
            // directions are right and left
            if (pos.X < g.Width - 1)
            {
                // try going right
                var newPos = pos + (1, 0);
                var newRisk = riskSoFar + g[newPos];
                toTry.Add((newPos, newRisk));
            }
            if (pos.Y < g.Height - 1)
            {
                // try going down
                var newPos = pos + (0, 1);
                var newRisk = riskSoFar + g[newPos];
                toTry.Add((newPos, newRisk));
            }

            foreach(var (newPos, newRisk) in toTry.OrderBy(t => t.Item2))
            {
                if (newRisk < bestRisk)
                {
                    if (newPos.Equals(target))
                    {
                        Console.WriteLine($"Best so far {newRisk}");
                        bestRisk = newRisk;
                        yield return newRisk;
                    }
                    else
                    {
                        foreach (var path in FindPaths(newPos, newRisk, g))
                            yield return path;
                    }
                }
                // else longer than the best, abandon this route
            }
        }

    }

}
