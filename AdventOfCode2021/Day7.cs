using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{

    public class Day7 : ISolver
    {
        public (string, string) ExpectedResult => ("342730", "92335207");

        public (string, string) Solve(string[] input)
        {
            var crabPos = input[0].Split(',').Select(int.Parse).ToArray();

            int minFuelA = FindMinFuel(crabPos, d => d);
            int minFuelB = FindMinFuel(crabPos, d => Enumerable.Range(1,d).Sum());

            return ($"{minFuelA}", $"{minFuelB}");
        }

        private static int FindMinFuel(int[] crabPos, Func<int,int> cost)
        {
            var minFuel = int.MaxValue;
            var cheapestPos = -1;
            for (int pos = crabPos.Min(); pos <= crabPos.Max(); pos++)
            {
                var fuel = 0;
                for (int crab = 0; crab < crabPos.Length; crab++)
                {
                    fuel += cost(Math.Abs(pos - crabPos[crab]));
                }
                if (fuel < minFuel)
                {
                    minFuel = fuel;
                    cheapestPos = pos;
                }
            }

            return minFuel;
        }

    }

}
