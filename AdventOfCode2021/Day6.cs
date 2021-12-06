using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{

    public class Day6 : ISolver
    {
        public (string, string) ExpectedResult => ("362346", "");

        public (string, string) Solve(string[] input)
        {
            return ($"{CountFish(input[0], 80)}", $"");
        }

        public int CountFish(string input, int days)
        {
            var fish = input.Split(',').Select(int.Parse);
            
            return fish.Select(f => Simulate(f, days)).Sum();
        }

        public int Simulate(int value, int days)
        {
            int fish = 1;
            while(days > 0)
            {
                if(value == 0)
                {
                    value = 6;
                    fish += Simulate(8, days-1);
                }
                else
                {
                    value--;
                }                
                days--;
            }
            return fish;
        }

    }

}
