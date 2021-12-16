using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{

    public class Day16 : ISolver
    {
        public (string, string) ExpectedResult => ("981", "299227024091");

        public (string, string) Solve(string[] input)
        {
            var packet = ParsePacket(input[0]);
            return ($"{GetVersionCount(packet)}", $"{packet.Value}");
        }

        public int GetVersionCount(string packetHex)
        {            
            return GetVersionCount(ParsePacket(packetHex));
        }

        private int GetVersionCount(Packet p)
        {
            if (p is OperatorPacket c)
            {
                return c.PacketVersion + c.SubPackets.Sum(s => GetVersionCount(s));
            }
            return p.PacketVersion;

        }

        public Packet ParsePacket(string packetHex)
        {
            var packetBinary = HexToBinary(packetHex);
            var (p, _) = ParsePacket(packetBinary, 0);
            return p;
        }

        private (Packet,int) ParsePacket(string input, int startPos)
        {
            var version = Convert.ToInt32(input.Substring(startPos, 3), 2);
            startPos += 3;
            var typeId = Convert.ToInt32(input.Substring(startPos, 3), 2);
            startPos += 3;
            if (typeId == 4)
            {
                var literalValue = 0L;
                var final = false;
                do
                {
                    if (input[startPos] == '0') final = true;
                    var val = Convert.ToInt32(input.Substring(startPos+1, 4), 2);
                    literalValue <<= 4;
                    literalValue |= val;                    
                    startPos += 5;
                } while (!final);
                return (new LiteralPacket(version, literalValue), startPos);
            }
            else
            {
                var p = new OperatorPacket();
                p.PacketType = typeId;
                p.PacketVersion = version;
                // its an operator
                var lengthType = input[startPos];
                startPos++;
                if (lengthType == '0')
                {
                    // next 15 bits are length of sub packets
                    var subPacketLength = Convert.ToInt32(input.Substring(startPos, 15), 2);
                    startPos += 15;
                    var subPacketsEnd = startPos + subPacketLength;
                    while (startPos < subPacketsEnd)
                    {
                        var (next, newPos) = ParsePacket(input, startPos);
                        startPos = newPos;
                        p.SubPackets.Add(next);
                    }
                }
                else
                {
                    var subPacketCount = Convert.ToInt32(input.Substring(startPos, 11), 2);
                    startPos += 11;
                    for (int n = 0; n < subPacketCount; n++)
                    {
                        var (next, newPos) = ParsePacket(input, startPos);
                        startPos = newPos;
                        p.SubPackets.Add(next);
                    }
                }
                return (p, startPos);
            }
        }

        public abstract class Packet
        {
            

            public int PacketVersion { get; set; }
            public int PacketType { get; set; }
            public abstract long Value { get; }
        }

        public class LiteralPacket : Packet
        {
            private readonly long value;

            public LiteralPacket(int packetVersion, long value)
            {
                base.PacketVersion = packetVersion;
                base.PacketType = 4;
                this.value = value;
            }
            public override long Value
            {
                get { return value; }
            }
        }
        public class OperatorPacket : Packet
        {
            public OperatorPacket()
            {
                SubPackets = new List<Packet>();
            }
            public List<Packet> SubPackets { get; }

            public override long Value
            {
                get
                {
                    switch (PacketType)
                    {
                        case 0://sum
                            return SubPackets.Sum(sp => sp.Value);
                        case 1:// product
                            return SubPackets.Select(sp => sp.Value).Aggregate(1L, (a, b) => a * b);
                        case 2: // min
                            return SubPackets.Min(sp => sp.Value);
                        case 3: // max
                            return SubPackets.Max(sp => sp.Value);
                        case 5: // greater than
                            return SubPackets[0].Value > SubPackets[1].Value ? 1 : 0;
                        case 6: // less than
                            return SubPackets[0].Value < SubPackets[1].Value ? 1 : 0;
                        case 7: // equal
                            return SubPackets[0].Value == SubPackets[1].Value ? 1 : 0;
                        default:
                            throw new InvalidOperationException($"Unknown packet type {this.PacketType}");
                    }
                }
            }
        }


        public string HexToBinary(string input)
        {
            var sb = new StringBuilder();
            foreach (var c in input)
            {               
                sb.Append(Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0'));
            }
            return sb.ToString();
        }
    }

}
