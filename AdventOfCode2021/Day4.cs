using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
    class Grid
    {
        private int[,] numbers = new int[5,5];
        private bool[,] hits = new bool[5,5];
        public Grid(IEnumerable<string> lines)
        {
            var row = 0;
            foreach(var line in lines.Skip(1))
            {
                var col = 0;
                foreach (var n in line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).Select(int.Parse))
                    numbers[row, col++] = n;
                row++;
            }
        }

        public void MarkNumber(int n)
        {
            for(var r = 0; r < numbers.GetLength(0); r++)
                for(var c = 0; c < numbers.GetLength(1); c++)
                    if (numbers[r, c] == n)
                        hits[r, c] = true;
        }

        public bool IsBingo()
        {
            
            return
                // any row where all the columns are hits
                Enumerable.Range(0, numbers.GetLength(0)).Any(row => Enumerable.Range(0, numbers.GetLength(1)).All(col => hits[row, col]))
                ||
                // any column where all the rows are hits
                Enumerable.Range(0, numbers.GetLength(1)).All(col => Enumerable.Range(0, numbers.GetLength(0)).All(row => hits[row, col]));
        }

        public int Score(int lastNumber)
        {
            int score = 0;
            for (var r = 0; r < numbers.GetLength(0); r++)
                for (var c = 0; c < numbers.GetLength(1); c++)
                    if (!hits[r, c])
                        score+= numbers[r,c];
            return score * lastNumber;
        }
    }


    public class Day4 : ISolver
    {
        private int[] numbers;
        private Grid[] grids;

        public (string, string) ExpectedResult => ("55770", "");

        public (string, string) Solve(string[] input)
        {
            numbers = input[0].Split(',').Select(int.Parse).ToArray();
            grids = input.Skip(1).Batch(6).Select(b => new Grid(b)).ToArray();


            return ($"{Part1()}", $"{Part2(input)}");
        }


        long Part1()
        {

            foreach (var n in numbers)
            {
                foreach (var g in grids)
                {
                    g.MarkNumber(n);
                    if (g.IsBingo())
                    {
                        return g.Score(n);
                    }
                }
            }
            throw new InvalidOperationException("No winner!");
        }

        long Part2(IEnumerable<string> input)
        {
            return 0;
        }
    }
}
