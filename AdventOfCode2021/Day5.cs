using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{

    public class Day5 : ISolver
    {
        public (string, string) ExpectedResult => ("5608", "20299");
        private Grid<int> grid;

        public (string, string) Solve(string[] input)
        {
            int atLeastTwo = Solver(input,false);
            int part2 = Solver(input, true);

            return ($"{atLeastTwo}", $"{part2}");
        }

        private int Solver(string[] input, bool includeDiagonal)
        {
            var lines = input.Select(x => x.Split(' '))
                            .Select(parts => new { From = parts[0].Split(',').Select(int.Parse).ToArray(), To = parts[2].Split(',').Select(int.Parse).ToArray() })
                            .ToArray();
            var maxX = lines.Max(l => Math.Max(l.From[0], l.To[0])) + 1;
            var maxY = lines.Max(l => Math.Max(l.From[1], l.To[1])) + 1;

            grid = new Grid<int>(maxX, maxY);
            foreach (var line in lines)
            {
                var x1 = Math.Min(line.From[0], line.To[0]);
                var x2 = Math.Max(line.From[0], line.To[0]);
                var y1 = Math.Min(line.From[1], line.To[1]);
                var y2 = Math.Max(line.From[1], line.To[1]);

                if (line.From[0] == line.To[0]) // a vertical line - same X
                {
                    for (var y = y1; y <= y2; y++)
                    {
                        grid[line.From[0], y]++;
                    }
                }
                else if (line.From[1] == line.To[1]) // a horizontal line - same Y
                {
                    for (var x = x1; x <= x2; x++)
                    {
                        grid[x, line.From[1]]++;
                    }
                }
                else if(includeDiagonal)
                {
                    var deltaX = line.From[0] < line.To[0] ? 1 : -1;
                    var deltaY = line.From[1] < line.To[1] ? 1 : -1;
                    // must be horizontal
                    for (int x = line.From[0], y = line.From[1]; 
                        ((deltaY == 1) && (y <= line.To[1])) || ((deltaY == -1) && (y >= line.To[1])); 
                        y += deltaY, x+= deltaX)
                    {
                        grid[x, y]++;
                    }
                }
            }
            var atLeastTwo = 0;
            for (var x = 0; x < maxX; x++)
                for (var y = 0; y < maxY; y++)
                    if (grid[x, y] >= 2)
                        atLeastTwo++;
            return atLeastTwo;
        }
    }

    class Grid<T>
    {
        private T[,] items;
        public Grid(int x, int y)
        {
            items = new T[x, y];
        }
        public T this[int x, int y]
        {
            get { return items[x,y]; }
            set { items[x, y] = value; }
        }

        // for now, just the four neighbours on the grid
        public IEnumerable<T> Neighbours(int x, int y)
        {
            // left
            if (x >= 1) yield return items[x - 1, y];
            // top
            if (y >= 1) yield return items[x, y - 1];
            // right
            if (x < items.GetLength(0) - 1) yield return items[x + 1, y];
            // bottom
            if (y < items.GetLength(1) - 1) yield return items[x, y + 1];

        }
    }

}
