using AdventOfCode2021;
using NUnit.Framework;
using System.Linq;

namespace UnitTests
{
    public class Day10Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]".Split("\r\n");
            var solver = new Day10();
            var solution = solver.Solve(testInput);
            Assert.That(solution, Is.EqualTo(("26397", "")));
        }


    }
}