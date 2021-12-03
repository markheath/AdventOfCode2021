using AdventOfCode2021;
using NUnit.Framework;

namespace UnitTests
{
    public class Day3Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"00100
11110
10110
10111
10101
01111
00111
11100
10000
11001
00010
01010".Split("\r\n");
            var solver = new Day3();
            var solution = solver.Solve(testInput);
            Assert.AreEqual(("198", ""), solution);
        }
    }
}