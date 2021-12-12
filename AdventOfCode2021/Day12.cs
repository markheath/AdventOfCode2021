using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{

    public class Day12 : ISolver
    {
        public (string, string) ExpectedResult => ("4885", "");

        public (string, string) Solve(string[] input)
        {
            var connections =
            input.SelectMany(x => x.Split('-'))
                .ToHashSet()
                .ToDictionary(x => x, x => new HashSet<string>());


            foreach (var connection in input.Select(x => x.Split('-')))           
            {
                connections[connection[0]].Add(connection[1]);
                connections[connection[1]].Add(connection[0]);
            }

            var routeCount = FindRoutes(connections, "start").Count();
            return ($"{routeCount}", $"");
        }


        // needs to be depth-first
        private IEnumerable<string> FindRoutes(Dictionary<string, HashSet<string>> map, string from, string currentRoute = "start,")
        {
            // valid places to go are big caves (upper) or anywhere we've not been yet
            foreach(var next in map[from].Where(d => char.IsUpper(d[0]) || !currentRoute.Contains(d)))
            {
                if (next == "end") yield return currentRoute + next;
                else foreach (var r in FindRoutes(map, next, currentRoute + next + ",")) yield return r;
            }
        }

    }
}
