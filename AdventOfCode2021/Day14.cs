using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{

    public class Day14 : ISolver
    {
        public (string, string) ExpectedResult => ("2375", "1976896901756");
        private Dictionary<string, string> instructions;

        public (string, string) Solve(string[] input)
        {
            var start = input[0];
            instructions = input.Skip(2)
                .Select(s => s.Split(' '))
                .ToDictionary(p => p[0], p => p[2]);

            var part1 = FastSolver(start, 10); //Part1Solver(start);
            var part2 = FastSolver(start, 40);


            return ($"{part1}", $"{part2}");
        }

        private long FastSolver(string start, int iterations)
        {
            var pairCounter = new Counter<string>(Pairs(start));
            var letterCounts = new Counter<char>(start);

            foreach(var n in Enumerable.Range(1, iterations))
            {
                pairCounter = GetPairCounts(pairCounter, letterCounts);
            }

            return letterCounts.Max(k => k.Value) - letterCounts.Min(k => k.Value);
        }

        public int Part1Solver(string start)
        {
            // 10 iterations
            for (int iteration = 0; iteration < 10; iteration++)
            {
                var next = new StringBuilder();
                // look at each pair in turn
                foreach (var pair in Pairs(start))
                {
                    next.Append(pair[0]);
                    if (instructions.TryGetValue(pair, out var insert))
                    {
                        next.Append(insert);
                    }
                }
                next.Append(start[start.Length - 1]);
                start = next.ToString();
                if (start.Length < 80)
                    Console.WriteLine($"{iteration + 1}: {start}");
            }
            var counts = start.CountBy(c => c);
            var diff = counts.Max(c => c.Value) - counts.Min(c => c.Value);
            return diff;
        }


        public Counter<string> GetPairCounts(Counter<string> pairCounts, Counter<char> letterCounts)
        {
            var outCounts = new Counter<string>();
            foreach (var (pair,count) in pairCounts)
            {
                var insert = instructions[pair]; // letter to insert
                outCounts.Add($"{pair[0]}{insert}", count);
                outCounts.Add($"{insert}{pair[1]}", count);
                letterCounts.Add(insert[0], count);
            }
            return outCounts;
        }


        private IEnumerable<string> Pairs(string x)
        {
            for (int n = 0; n < x.Length - 1; n++)
                yield return x.Substring(n, 2);
        }


    }

    public class Counter<T> : IEnumerable<KeyValuePair<T,long>>
    {
        public Counter()
        {

        }

        public Counter(IEnumerable<T> initial)
        {
            foreach(var item in initial)
            {
                Add(item, 1);
            }
        }

        private Dictionary<T, long> counts = new Dictionary<T, long>();

        public void Add(T item, long amount)
        {
            if(counts.TryGetValue(item, out var count))
            {
                counts[item] = count + amount;
            }
            else
            {
                counts[item] = amount;
            }
        }

        public IEnumerator<KeyValuePair<T, long>> GetEnumerator() => counts.GetEnumerator();
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => counts.GetEnumerator();
    }


}
