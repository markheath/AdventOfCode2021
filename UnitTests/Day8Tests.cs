using AdventOfCode2021;
using NUnit.Framework;
using System.Linq;

namespace UnitTests
{
    public class Day8Tests
    {
        [Test]
        public void SolveWithTestInput()
        {
            var testInput = @"be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe
edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc
fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg
fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb
aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea
fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb
dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe
bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef
egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb
gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce".Split("\r\n");
            var solver = new Day8();
            var solution = solver.Solve(testInput);
            Assert.That(solution, Is.EqualTo(("26", "61229")));
        }

        [Test]
        public void CreateLookup()
        {
            var solver = new Day8();
            var lookup = solver.CreateLookup("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab".Split(' '));
            Assert.AreEqual(8, lookup["acedgfb"]);
            Assert.AreEqual(5, lookup["cdfbe"]);
            Assert.AreEqual(2, lookup["gcdfa"]);
        }

    }
}