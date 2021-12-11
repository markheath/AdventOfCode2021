﻿using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{

    class Grid<T>
    {
        public static Grid<int> ParseToGrid(string[] input)
        {
            var grid = new Grid<int>(input[0].Length, input.Length);
            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                    grid[(x, y)] = input[y][x] - '0';
            }
            return grid;
        }

        private T[,] items;
        public Grid(int x, int y)
        {
            items = new T[x, y];
        }
        public T this[Coord c]
        {
            get { return items[c.X,c.Y]; }
            set { items[c.X, c.Y] = value; }
        }

        public int Width => items.GetLength(0);
        public int Height => items.GetLength(1);

        public bool IsInGrid(Coord point) => point.X >= 0 && point.X < Width && point.Y >= 0 && point.Y < Height;

        public IEnumerable<Coord> AllPositions()
        {
            for(int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    yield return new Coord(x, y);
        }

        private readonly IEnumerable<Coord> horizontalNeighbours = new[] { new Coord(-1, 0), new Coord(1, 0), new Coord(0, -1), new Coord(0, 1) };
        private readonly IEnumerable<Coord> diagonalNeigbours = new[] { new Coord(-1, -1), new Coord(1, 1), new Coord(1, -1), new Coord(-1, 1) };
        
        public IEnumerable<Coord> Neighbours(Coord p, bool includeDiagonals = false)
        {
            var neighbours = includeDiagonals ? horizontalNeighbours.Concat(diagonalNeigbours) : horizontalNeighbours;

            return neighbours.Select(n => p + n).Where(IsInGrid);
        }
    }

}
