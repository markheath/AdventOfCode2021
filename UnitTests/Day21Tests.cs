using AdventOfCode2021;
using NUnit.Framework;
using System.Linq;

namespace UnitTests;

public class Day21Tests
{

    [Test]
    public void SolveWithTestInput()
    {
        var input = @"Player 1 starting position: 4
Player 2 starting position: 8".Split("\r\n");
        var solver = new Day21();
        var solution = solver.Solve(input);
        Assert.That(solution, Is.EqualTo(("739785", "444356092776315")));
    }

}
