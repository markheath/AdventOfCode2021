using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{

    public class Day12 : ISolver
    {
        public (string, string) ExpectedResult => ("4885", "117095");

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

            var routeCount = FindRoutes(connections, 1, "start").Count();
            var routes2 = FindRoutes(connections, 2, "start").ToList();
            /*foreach (var r in routes2)
                Console.WriteLine(r);*/
            var routeCount2 = routes2.Count;
            return ($"{routeCount}", $"{routeCount2}");
        }

        private int Count(string s, string search)
        {
            var first = s.IndexOf(search);
            if (first == -1) return 0;
            if (s.LastIndexOf(search) == first) return 1;
            return 2; // more than 1
        }

        // needs to be depth-first
        private IEnumerable<string> FindRoutes(Dictionary<string, HashSet<string>> map, int maxSmallVisits, string from, string currentRoute = "start,")
        {
            // valid places to go are big caves (upper) or anywhere we've not been yet
            foreach(var next in map[from]
                .Where(d => d != "start"))
            {
                if (next == "end")
                {
                    yield return currentRoute + next;
                }
                else
                {
                    var canVisit = char.IsUpper(next[0]);
                    var allowedSmallVisits = maxSmallVisits;
                    if (!canVisit)
                    {
                        // this is a small cave, how many times
                        var previousVisits = Count(currentRoute, next + ",");
                        canVisit = previousVisits < maxSmallVisits;
                        if (previousVisits == 1) // we're doing our second visit, so max drops to 1
                            allowedSmallVisits = 1;

                    }
                    if (canVisit)
                    {
                        foreach (var r in FindRoutes(map, allowedSmallVisits, next, currentRoute + next + ","))
                        {
                            yield return r;
                        }
                    }
                }
            }
        }

    }
}
