using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2021;


public class Day22 : ISolver
{
    public (string, string) ExpectedResult => ("", "");
    
    // record RebootStep(bool targetState, )

    public (string Part1, string Part2) Solve(string[] input)
    {
        var rebootSteps = input.Select(line => new { On = line[1] == 'n', Points = Regex.Matches(line,@"-?\d+").Select(m => Int32.Parse(m.Value)).ToArray() }).ToArray();
        var onCubes = new HashSet<Coord3>();
        foreach(var step in rebootSteps.Where(s => s.Points.All(p => p <= 50 && p >= -50)))
        {
            for(var x = step.Points[0]; x <= step.Points[1]; x++)
                for (var y = step.Points[2]; y <= step.Points[3]; y++)
                    for (var z = step.Points[4]; z <= step.Points[5]; z++)
                        
                        if (step.On)
                            onCubes.Add((x,y,z));
                        else
                            onCubes.Remove((x,y,z));
        }

        return ($"{onCubes.Count}", $"");
    }

    
}
