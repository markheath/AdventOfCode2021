using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{

    class Grid<T>
    {
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

        

        // for now, just the four neighbours on the grid
        public IEnumerable<Coord> Neighbours(Coord p)
        {
            var neighbours = new[] { new Coord(-1, 0), new Coord(1, 0), new Coord(0, -1), new Coord(0, 1) };
            return neighbours.Select(n => p + n).Where(IsInGrid);
        }
    }

}
