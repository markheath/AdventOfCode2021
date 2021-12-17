using AdventOfCode2021;
using NUnit.Framework;
using System.Linq;

namespace UnitTests
{
    public class Day17Tests
    {

        [Test]
        public void SolveWithTestInput()
        {
            var input = @"target area: x=20..30, y=-10..-5";
            var solver = new Day17();
            var solution = solver.Solve(new[] { input });
            Assert.That(solution, Is.EqualTo(("45", "112")));
        }

    }
}