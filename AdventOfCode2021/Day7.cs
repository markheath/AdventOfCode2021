using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{

    public class Day7 : ISolver
    {
        public (string, string) ExpectedResult => ("342730", "");

        public (string, string) Solve(string[] input)
        {
            var crabPos = input[0].Split(',').Select(int.Parse).ToArray();

            var minFuel = int.MaxValue;
            var cheapestPos = -1;
            for(int pos = crabPos.Min(); pos <= crabPos.Max(); pos++)
            {
                var fuel = 0;
                for(int crab = 0; crab < crabPos.Length; crab++)
                {
                    fuel += Math.Abs(pos - crabPos[crab]);
                }
                if (fuel < minFuel)
                {
                    minFuel = fuel;
                    cheapestPos = pos;
                }
            }

            return ($"{minFuel}", $"");
        }


    }

}
