using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{

    public class Day14 : ISolver
    {
        public (string, string) ExpectedResult => ("2375", "");

        public (string, string) Solve(string[] input)
        {
            var start = input[0]; //PolymerElement.Parse(input[0]);
            var instructions = input.Skip(2)
                .Select(s => s.Split(' '))
                .ToDictionary(p => p[0], p => p[2]);


            // 10 iterations
            for(int iteration =0; iteration < 10; iteration++)
            {
                var next = new StringBuilder();
                // look at each pair in turn
                for (int n = 0; n < start.Length - 1; n++)
                {
                    next.Append(start[n]);
                    if (instructions.TryGetValue(start.Substring(n,2), out var insert))
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
            return ($"{diff}", $"");
        }
    }
    /*
    class PolymerElement
    { 
        public static PolymerElement Parse(string input)
        {
            PolymerElement first = null;
            PolymerElement prev = null;
            foreach (var c in input)
            {
                var element = new PolymerElement() { Element = c };
                if (first == null) first = element;
                if (prev != null) prev.Next = element;
                prev = element;
            }
            return first;
        }

        public char Element { get; set; }
        public PolymerElement Next { get; set; }
    }*/

}
