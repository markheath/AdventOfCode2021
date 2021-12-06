using AdventOfCode2021;
using NUnit.Framework;
using System.Linq;

namespace UnitTests
{
    public class Day6Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"3,4,3,1,2".Split("\r\n");
            var solver = new Day6();
            var solution = solver.CountFish(testInput[0],18);
            Assert.That(solution, Is.EqualTo(26));
        }
    }
}