using AdventOfCode2021;
using NUnit.Framework;

namespace UnitTests
{
    public class Day1Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"199
200
208
210
200
207
240
269
260
263".Split("\r\n");
            var day1 = new Day1();
            var solution = day1.Solve(testInput);
            Assert.AreEqual(("7", "5"), solution);
        }
    }
}