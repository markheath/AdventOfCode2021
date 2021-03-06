using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    class Program
    {
        static void Main(string[] args)
        {
            var (solver,input) = Utils.FindAllSolvers().First();
            Console.WriteLine($"Solving for day {solver.GetType().Name[3..]}");
            var sw = Stopwatch.StartNew();
            var (a,b) = solver.Solve(input);
            sw.Stop();
            Console.WriteLine($"ResultA: {a}");
            Console.WriteLine($"ResultB: {b}");

            if (solver.ExpectedResult != (a,b))
            {
                Console.WriteLine($"Error after {sw.ElapsedMilliseconds}ms! Expected: {solver.ExpectedResult}");
            }
            else
            {
                Console.WriteLine($"Success! {sw.ElapsedMilliseconds}ms");
            }
        }


    }
}
