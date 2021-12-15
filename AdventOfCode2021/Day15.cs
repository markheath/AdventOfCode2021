using System;
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
            var bigGrid = Expand(grid);
            //Console.WriteLine(bigGrid);
            var best2 = MinCost((0, 0), (bigGrid.Width - 1, bigGrid.Height - 1), bigGrid);
            return ($"{best}", $"{best2}");
        }

        private Grid<int> Expand(Grid<int> inputGrid)
        {
            var outputGrid = new Grid<int>(inputGrid.Width * 5, inputGrid.Height * 5);
            for (var x = 0; x < outputGrid.Width; x++)
            {
                for(var y = 0; y < outputGrid.Height; y++)
                {
                    var repeat = x/inputGrid.Width + y/inputGrid.Height;
                    var value = inputGrid[(x % inputGrid.Width,y % inputGrid.Height)] + repeat;
                    if (value >= 10) value -= 9;
                    if (value >= 10) throw new InvalidOperationException("oops");
                    outputGrid[(x, y)] = value;
                }
            }
            return outputGrid;
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
