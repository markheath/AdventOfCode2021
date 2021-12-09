using AdventOfCode2021;
using NUnit.Framework;
using System.Linq;

namespace UnitTests
{
    public class Day9Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"2199943210
3987894921
9856789892
8767896789
9899965678".Split("\r\n");
            var solver = new Day9();
            var solution = solver.Solve(testInput);
            Assert.That(solution, Is.EqualTo(("15", "1134")));
        }


    }
}