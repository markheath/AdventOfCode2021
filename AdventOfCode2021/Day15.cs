using System.Collections.Generic;

namespace AdventOfCode2021
{

    public class Day15 : ISolver
    {
        public (string, string) ExpectedResult => ("613", "");

        public (string, string) Solve(string[] input)
        {
            var grid = Grid<int>.ParseToGrid(input);
            var best = MinCost((0, 0), (grid.Width - 1, grid.Height - 1), grid);
            return ($"{best}", $"");
        }

        // my pathfinder algorithm was too slow, so took inspiration from this solution by DenverCoder1 to optimise
        // https://github.com/DenverCoder1/Advent-of-Code-2021/blob/main/Day-15/part1.py
        private int MinCost(Coord start, Coord target, Grid<int> map)
        {
            var lowestCost = new Grid<int>(map.Width, map.Height, int.MaxValue);
            lowestCost[start] = 0;
            var queue = new Queue<Coord>();
            queue.Enqueue(start);
            while(queue.Count > 0)
            {
                var current = queue.Dequeue();
                foreach(var neighbour in map.Neighbours(current))
                {
                    var neighbourCost = lowestCost[neighbour];
                    var costToNeighbour = lowestCost[current] + map[neighbour];
                    if (neighbourCost > costToNeighbour)
                    {
                        lowestCost[neighbour] = costToNeighbour;
                        queue.Enqueue(neighbour);
                    }
                }
            }
            return lowestCost[target];
        }



    }

}
