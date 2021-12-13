using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{

    public class Day13 : ISolver
    {
        public (string, string) ExpectedResult => ("602", "");

        public (string, string) Solve(string[] input)
        {
            var points = input.TakeWhile(x => !String.IsNullOrEmpty(x))
                    .Select(x => x.Split(','))
                    .Select(x => new Coord(int.Parse(x[0]), int.Parse(x[1])))
                    .ToArray();
            var maxX = points.Max(c => c.X);
            var maxY = points.Max(c => c.Y);
            var grid = new Grid<char>(maxX + 1, maxY + 1);
            foreach (var p in grid.AllPositions())
            {
                grid[p] = '.';
            }
            foreach (var p in points)
            {
                grid[p] = '#';
            }
            //Console.WriteLine(grid);
            var folds = input.Where(x => x.StartsWith("fold along "))
                .Select(x => x.Substring(11).Split("="))
                .Select(x => new { Axis = x[0], Pos = int.Parse(x[1]) })
                .ToList();
            foreach(var fold in folds)
            {
                if (fold.Axis == "x")
                {
                    for (var x = fold.Pos + 1; x < grid.Width; x++)
                    {
                        for(var y  = 0; y < grid.Height; y++)
                        {
                            if (grid[(x, y)] == '#')
                            {
                                grid[(fold.Pos - (x - fold.Pos), y)] = '#';
                            }
                                
                        }
                    }
                    // shrink the grid
                    grid.Width = fold.Pos;
                }
                else
                {
                    for (var y = fold.Pos + 1; y < grid.Height; y++)
                    {
                        for (var x = 0; x < grid.Width; x++)
                        {
                            if (grid[(x, y)] == '#')
                            {
                                grid[(x, fold.Pos - (y - fold.Pos))] = '#';
                            }
                        }
                    }
                    // shrink the grid
                    grid.Height = fold.Pos;
                }
                //Console.WriteLine("===");
                //Console.WriteLine(grid);
                break; // just first fold for now
            }

            var totalPoints = grid.AllPositions().Count(p => grid[p] == '#');

            return ($"{totalPoints}", $"");
        }
    }
}
