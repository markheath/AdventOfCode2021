using AdventOfCode2021;
using NUnit.Framework;
using System.Linq;

namespace UnitTests
{
    public class Day14Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C".Split("\r\n");
            var solver = new Day14();
            var solution = solver.Solve(testInput);
            Assert.That(solution, Is.EqualTo(("1588", "2188189693529")));
        }


    }
}