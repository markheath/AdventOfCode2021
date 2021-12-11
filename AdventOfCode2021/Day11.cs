using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{

    public class Day11 : ISolver
    {
        public (string, string) ExpectedResult => ("1615", "249");

        public (string, string) Solve(string[] input)
        {
            var grid = Grid<int>.ParseToGrid(input);
            var total = Enumerable.Range(1, 100).Sum(n => Step(grid));

            var firstSyncFlash = Enumerable.Range(101, 1000).SkipWhile(n => Step(grid) != 100).First();

            return ($"{total}", $"{firstSyncFlash}");
        }

        private int Step(Grid<int> grid)
        {
            // increment energy level of each octopus
            foreach(var c in grid.AllPositions())
            {
                grid[c] += 1;
            }

            // every octopus level 10 is flashing and boosts all neihbours by 1
            var flashes =  ProcessFlashes(grid, grid.AllPositions().Where(p => grid[p] == 10).ToList());

            // reset flashes to 0
            foreach (var c in grid.AllPositions().Where(p => grid[p] == 10))
            {
                grid[c] = 0;
            }
            return flashes;
        }

        private int ProcessFlashes(Grid<int> grid, IEnumerable<Coord> flashPositions)
        {
            int flashes = 0;
            foreach(var flashPos in flashPositions)
            {
                flashes++;
                foreach(var neigbourPos in grid.Neighbours(flashPos, true).Where(p => grid[p] < 10))
                {
                    grid[neigbourPos]++;
                    var newFlashes = new List<Coord>();
                    if (grid[neigbourPos] == 10)
                        newFlashes.Add(neigbourPos);
                    flashes += ProcessFlashes(grid, newFlashes);
                }
            }
            return flashes;
        }
    }

}
