using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{

    public class Day9 : ISolver
    {
        public (string, string) ExpectedResult => ("594", "");

        public (string, string) Solve(string[] input)
        {
            var grid = ParseToGrid(input);
            var totalRisk = FindLowPoints(grid).Sum((p) => 1 + grid[p.x, p.y]);
            return ($"{totalRisk}", $"");
        }

        private IEnumerable<(int x,int y)> FindLowPoints(Grid<int> grid)
        {
            for (var y = 0; y < grid.Height; y++)
            {
                for (var x = 0; x < grid.Width; x++)
                {
                    if (IsLowPoint(grid[x, y], grid.Neighbours(x, y)))
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

        private static Grid<int> ParseToGrid(string[] input)
        {
            var grid = new Grid<int>(input[0].Length, input.Length);
            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                    grid[x, y] = input[y][x] - '0';
            }
            return grid;
        }


    }

}
