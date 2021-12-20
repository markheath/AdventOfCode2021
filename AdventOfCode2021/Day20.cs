using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{

    class InfiniteGrid
    {
        private HashSet<Coord> onPixels = new HashSet<Coord>();
        public int MinX { get; private set; }
        public int MinY { get; private set; }
        public int MaxX { get; private set; }
        public int MaxY { get; private set; }

        public int Count { get => onPixels.Count; }
        public bool OutOfBounds { get; }

        public InfiniteGrid(bool outOfBounds)
        {
            MinX = int.MaxValue;
            MinY = int.MaxValue;
            MaxX = int.MinValue;
            MaxY = int.MinValue;
            OutOfBounds = outOfBounds;
        }

        public static InfiniteGrid ParseImage(string[] input)
        {
            var grid = new InfiniteGrid(false);
            var y = 0;
            foreach (var line in input.Skip(2))
            {
                for (int x = 0; x < line.Length; x++)
                {
                    if (line[x] == '#') grid[new Coord(x, y)] = true;
                }
                y++;
            }

            return grid;
        }

        public bool this[Coord c] 
        {
            get
            {
                if (OutOfBounds && (c.X < MinX || c.Y < MinY|| c.X > MaxX || c.Y > MaxY))
                    return OutOfBounds;
                return onPixels.Contains(c);
            }
            set
            {
                if (value)
                {
                    MinX = Math.Min(MinX, c.X);
                    MaxX = Math.Max(MaxX, c.X);
                    MinY = Math.Min(MinY, c.Y);
                    MaxY = Math.Max(MaxY, c.Y);
                    onPixels.Add(c);
                }
                else
                {
                    onPixels.Remove(c);
                }
            }
        }

    }





    public class Day20 : ISolver
    {
        public (string, string) ExpectedResult => ("5489", "");

        public (string Part1, string Part2) Solve(string[] input)
        {
            var algorithm = input[0];
            var image = InfiniteGrid.ParseImage(input);
            //Print(image);
            foreach (var n in Enumerable.Range(0, 2))
            {
                image = Enhance(algorithm, image);
                Print(image);
            }

            return ($"{image.Count}", $"");
        }

        private InfiniteGrid Enhance(string algorithm, InfiniteGrid image)
        {
            var minX = image.MinX - 1;
            var maxX = image.MaxX + 1;
            var minY = image.MinY - 1;
            var maxY = image.MaxY + 1;
            var enhanced = new InfiniteGrid(algorithm[0] == '.' ? false : !image.OutOfBounds);
            for(var x = minX; x <= maxX; x++)
                for(var y = minY; y <= maxY; y++)
                    if (algorithm[GetIndex((x,y), image)] == '#')
                        enhanced[new Coord(x,y)] = true;
            return enhanced;
        }

        private void Print(InfiniteGrid image)
        {
            var minX = image.MinX;
            var maxX = image.MaxX;
            var minY = image.MinY;
            var maxY = image.MaxY;
            var enhanced = new HashSet<Coord>();
            Console.WriteLine();
            for (var y = minY; y <= maxY; y++)                
            {
                for (var x = minX; x <= maxX; x++)
                    Console.Write(image[(x,y)] ? '#' : '.');
                Console.WriteLine();
            }

        }

        static readonly IEnumerable<Coord> surrounding = new List<Coord>() { (-1, -1), (0, -1), (1, -1), (-1, 0), (0, 0), (1, 0), (-1, 1), (0, 1), (1, 1) };
        private int GetIndex(Coord c, InfiniteGrid grid)
        {
            var binary = string.Concat(surrounding.Select(s => c + s).Select(c => grid[c] ? '1' : '0'));
            return Convert.ToInt32(binary, 2);
        }

    }
}