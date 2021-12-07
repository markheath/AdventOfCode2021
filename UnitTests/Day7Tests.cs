using AdventOfCode2021;
using NUnit.Framework;
using System.Linq;

namespace UnitTests
{
    public class Day7Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"16,1,2,0,4,2,7,1,2,14".Split("\r\n");
            var solver = new Day7();
            var solution = solver.Solve(testInput);
            Assert.That(solution, Is.EqualTo(("37", "168")));
        }

    }
}