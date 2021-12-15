using AdventOfCode2021;
using NUnit.Framework;
using System.Linq;

namespace UnitTests
{
    public class Day15Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"1163751742
1381373672
2136511328
3694931569
7463417111
1319128137
1359912421
3125421639
1293138521
2311944581".Split("\r\n");
            var solver = new Day15();
            var solution = solver.Solve(testInput);
            Assert.That(solution, Is.EqualTo(("40", "")));
        }


    }
}