using AdventOfCode2021;
using NUnit.Framework;
using System.Linq;

namespace UnitTests
{
    public class Day11Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526".Split("\r\n");
            var solver = new Day11();
            var solution = solver.Solve(testInput);
            Assert.That(solution, Is.EqualTo(("1656", "")));
        }


    }
}