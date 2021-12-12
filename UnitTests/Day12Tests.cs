using AdventOfCode2021;
using NUnit.Framework;
using System.Linq;

namespace UnitTests
{
    public class Day12Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"start-A
start-b
A-c
A-b
b-d
A-end
b-end".Split("\r\n");
            var solver = new Day12();
            var solution = solver.Solve(testInput);
            Assert.That(solution, Is.EqualTo(("10","36")));
        }

        [Test]
        public void SolveWithTestInput2()
        {
            var testInput = @"dc-end
HN-start
start-kj
dc-start
dc-HN
LN-dc
HN-end
kj-sa
kj-HN
kj-dc".Split("\r\n");
            var solver = new Day12();
            var solution = solver.Solve(testInput);
            Assert.That(solution, Is.EqualTo(("19","103")));
        }

        [Test]
        public void SolveWithTestInput3()
        {
            var testInput = @"fs-end
he-DX
fs-he
start-DX
pj-DX
end-zg
zg-sl
zg-pj
pj-he
RW-he
fs-DX
pj-RW
zg-RW
start-pj
he-WI
zg-he
pj-fs
start-RW".Split("\r\n");
            var solver = new Day12();
            var solution = solver.Solve(testInput);
            Assert.That(solution, Is.EqualTo(("226", "3509")));
        }

    }
}