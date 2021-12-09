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
            var totalRisk = 0;
            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                { 
                    if (IsLowPoint(grid[x,y],grid.Neighbours(x,y)))
                    {
                        var riskLevel = 1 + grid[x, y];
                        totalRisk += riskLevel;
                    }
                }
            }
            return ($"{totalRisk}", $"");
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
