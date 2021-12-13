using AdventOfCode2021;
using NUnit.Framework;
using System.Linq;

namespace UnitTests
{
    public class Day13Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"6,10
0,14
9,10
0,3
10,4
4,11
6,0
6,12
4,1
0,13
10,12
3,4
3,0
8,4
1,10
2,14
8,10
9,0

fold along y=7
fold along x=5".Split("\r\n");
            var solver = new Day13();
            var solution = solver.Solve(testInput);
            Assert.That(solution, Is.EqualTo(("17", "")));
        }


    }
}