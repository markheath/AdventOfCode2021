using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{

    public class Day9 : ISolver
    {
        public (string, string) ExpectedResult => ("594", "858494");

        public (string, string) Solve(string[] input)
        {
            var grid = Grid<int>.ParseToGrid(input);
            var lowPoints = FindLowPoints(grid).ToList();
            var totalRisk = lowPoints.Sum(p => 1 + grid[p]);

            var basinSizes = lowPoints.Select(point => FindBasinSize(grid, point)).ToList();
            var part2 = basinSizes.OrderByDescending(n => n).Take(3).Aggregate((a, b) => a * b);

            return ($"{totalRisk}", $"{part2}");
        }

        private long FindBasinSize(Grid<int> grid, (int, int) startingPoint, HashSet<(int,int)> currentPoints = null)
        {
            if (currentPoints == null) currentPoints = new HashSet<(int, int)>();

            foreach(var n in grid.Neighbours(startingPoint))
            {
                if (!currentPoints.Contains(n) && grid[n] != 9)
                {
                    currentPoints.Add(n);
                    FindBasinSize(grid, n, currentPoints);
                }
            }
            return currentPoints.Count;
        }

        private IEnumerable<(int x,int y)> FindLowPoints(Grid<int> grid)
        {
            for (var y = 0; y < grid.Height; y++)
            {
                for (var x = 0; x < grid.Width; x++)
                {
                    if (IsLowPoint(grid[(x, y)], grid.Neighbours((x, y)).Select(c => grid[c])))
                    {
                        yield return (x, y);
                    }
                }
            }

        }

        private bool IsLowPoint(int v, IEnumerable<int> neighbours)
        {
            return neighbours.All(n => n > v);
        }




    }

}
