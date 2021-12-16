using AdventOfCode2021;
using NUnit.Framework;
using System.Linq;

namespace UnitTests
{
    public class Day16Tests
    {
        [TestCase("8A004A801A8002F478", 16)]
        [TestCase("620080001611562C8802118E34", 12)]
        [TestCase("C0015000016115A2E0802F182340", 23)]
        [TestCase("A0016C880162017C3686B18A3D4780", 31)]

        public void SolveWithTestInput(string inputHex, int versionSum)
        {
            var solver = new Day16();
            var solution = solver.GetVersionCount(inputHex);
            Assert.That(solution, Is.EqualTo(versionSum));
        }

        [Test]
        public void ParseSamplePacket()
        {
            var solver = new Day16();
            var p = solver.ParsePacket("38006F45291200");

            
        }
    }
}