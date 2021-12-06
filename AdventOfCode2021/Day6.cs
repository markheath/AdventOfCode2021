using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{

    public class Day6 : ISolver
    {
        public (string, string) ExpectedResult => ("362346", "1639643057051");

        public (string, string) Solve(string[] input)
        {
            return ($"{CountFish(input[0], 80)}", $"{CountFish(input[0], 256)}");
        }

        public long CountFish(string input, int days)
        {
            var fish = input.Split(',').Select(int.Parse);
            
            return fish.Select(f => Simulate(f, days)).Sum();
        }

        private Dictionary<(int,int),long> memo = new Dictionary<(int,int),long>();

        public long Simulate(int value, int days)
        {
            var key = (value, days);
            if (memo.ContainsKey(key))
                return memo[key];

            long fish = 1;
            while(days > 0)
            {
                if(value == 0)
                {
                    value = 6;
                    fish += Simulate(8, days - 1);
                }
                else
                {
                    value--;
                }
                days--;
            }
            memo[key] = fish;
            return fish;
        }

    }

}
