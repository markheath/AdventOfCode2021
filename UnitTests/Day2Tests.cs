using AdventOfCode2021;
using NUnit.Framework;

namespace UnitTests
{
    public class Day2Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"forward 5
down 5
forward 8
up 3
down 8
forward 2".Split("\r\n");
            var day1 = new Day2();
            var solution = day1.Solve(testInput);
            Assert.AreEqual(("150", ""), solution);
        }
    }
}