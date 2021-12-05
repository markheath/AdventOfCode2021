using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{

    public class Day5 : ISolver
    {


        public (string, string) ExpectedResult => ("5608", "");
        private Grid<int> grid;

        public (string, string) Solve(string[] input)
        {
            
            var lines = input.Select(x => x.Split(' '))
                .Select(parts => new { From = parts[0].Split(',').Select(int.Parse).ToArray(), To = parts[2].Split(',').Select(int.Parse).ToArray() })
                .ToArray();
            var maxX = lines.Max(l => Math.Max(l.From[0], l.To[0])) + 1;
            var maxY = lines.Max(l => Math.Max(l.From[1], l.To[1])) + 1;
            
            grid = new Grid<int>(maxX, maxY);
            foreach(var line in lines)
            {
                if (line.From[0] == line.To[0]) // a vertical line - same X
                {
                    for(var y = Math.Min(line.From[1],line.To[1]); y <= Math.Max(line.From[1], line.To[1]); y++)
                    {
                        grid[line.From[0], y]++;
                    }
                }
                else if (line.From[1] == line.To[1]) // a horizontal line - same Y
                {
                    for (var x = Math.Min(line.From[0], line.To[0]); x <= Math.Max(line.From[0], line.To[0]); x++)
                    {
                        grid[x, line.From[1]]++;
                    }
                }
            }
            var atLeastTwo = 0;
            for(var x = 0; x < maxX; x++)
                for (var y = 0; y < maxY; y++)
                    if (grid[x,y] >= 2)
                        atLeastTwo++;

            return ($"{atLeastTwo}", $"");
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
    }

}
