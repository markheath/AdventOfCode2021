using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{
    class Grid
    {
        private int[,] numbers = new int[5,5];
        private bool[,] hits = new bool[5,5];
        private bool isDone;
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
            if (isDone) return;
            for(var r = 0; r < numbers.GetLength(0); r++)
                for(var c = 0; c < numbers.GetLength(1); c++)
                    if (numbers[r, c] == n)
                        hits[r, c] = true;
        }

        public bool IsBingo()
        {
            if (isDone) return false;
            var isBingo = 
                // any row where all the columns are hits
                Enumerable.Range(0, numbers.GetLength(0)).Any(row => Enumerable.Range(0, numbers.GetLength(1)).All(col => hits[row, col]))
                ||
                // any column where all the rows are hits
                Enumerable.Range(0, numbers.GetLength(1)).Any(col => Enumerable.Range(0, numbers.GetLength(0)).All(row => hits[row, col]));
            if (isBingo)
            {
                isDone = true;
            }
            return isBingo;
        }

        public override string ToString()
        {
            var s = new StringBuilder();
            for (var r = 0; r < numbers.GetLength(0); r++)
            {
                for (var c = 0; c < numbers.GetLength(1); c++)
                {
                    if (hits[r, c])
                        s.Append($"*{numbers[r, c]}*".PadRight(5));
                    else
                        s.Append($"{numbers[r, c]}".PadRight(5));

                }
                s.AppendLine();
            }
            return s.ToString();
        }

        public int Score(int lastNumber)
        {
            int score = 0;
            for (var r = 0; r < numbers.GetLength(0); r++)
                for (var c = 0; c < numbers.GetLength(1); c++)
                    if (!hits[r, c])
                        score+= numbers[r,c];
            //Console.WriteLine($"done after {lastNumber} with unmarked sum {score}");
            return score * lastNumber;
        }
    }


    public class Day4 : ISolver
    {
        private int[] numbers;
        private Grid[] grids;

        public (string, string) ExpectedResult => ("55770", "2980");

        public (string, string) Solve(string[] input)
        {
            numbers = input[0].Split(',').Select(int.Parse).ToArray();
            grids = input.Skip(1).Batch(6).Select(b => new Grid(b)).ToArray();

            var firstWinner = -1;
            var lastWinner = 0;
            foreach (var n in numbers)
            {
                foreach (var g in grids)
                {
                    g.MarkNumber(n);
                    if (g.IsBingo())
                    {
                        //Console.WriteLine(g);
                        var score = g.Score(n);
                        if (firstWinner == -1) firstWinner = score;
                        lastWinner = score;
                    }
                }
            }


            return ($"{firstWinner}", $"{lastWinner}");
        }

    }
}
