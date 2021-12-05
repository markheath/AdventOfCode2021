using AdventOfCode2021;
using NUnit.Framework;

namespace UnitTests
{
    public class Day5Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2".Split("\r\n");
            var solver = new Day5();
            var solution = solver.Solve(testInput);
            Assert.AreEqual(("5", "12"), solution);
        }
    }
}