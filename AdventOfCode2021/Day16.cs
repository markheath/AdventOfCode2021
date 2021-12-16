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
            return ($"{GetVersionSum(packet)}", $"{packet.Value}");
        }

        public int GetVersionSum(Packet p)
        {
            if (p is OperatorPacket c)
            {
                return c.PacketVersion + c.SubPackets.Sum(s => GetVersionSum(s));
            }
            return p.PacketVersion;
        }

        public Packet ParsePacket(string packetHex)
        {
            return ParsePacket(new BitStream(packetHex));
        }

        private static long ReadExtensibleNumber(BitStream bitStream)
        {
            var literalValue = 0L;
            var final = false;
            do
            {
                if (bitStream.ReadNumber(1) == 0) final = true;
                var val = bitStream.ReadNumber(4);
                literalValue <<= 4;
                literalValue |= val;
            } while (!final);
            return literalValue;
        }

        private Packet ParsePacket(BitStream bitStream)
        {
            var version = (int)bitStream.ReadNumber(3);
            var typeId = (int)bitStream.ReadNumber(3);
            if (typeId == 4)
            {
                return new LiteralPacket(version, ReadExtensibleNumber(bitStream));
            }
            else
            {
                var p = new OperatorPacket(version, typeId);
                // its an operator
                var lengthType = bitStream.ReadNumber(1);
                if (lengthType == 0)
                {
                    // next 15 bits are length of sub packets
                    var subPacketLength = bitStream.ReadNumber(15); 
                    var subPacketsEnd = bitStream.BitPosition + subPacketLength;
                    while (bitStream.BitPosition < subPacketsEnd)
                    {
                        var next = ParsePacket(bitStream);
                        p.SubPackets.Add(next);
                    }
                }
                else
                {
                    var subPacketCount = bitStream.ReadNumber(11);
                    for (int n = 0; n < subPacketCount; n++)
                    {
                        var next = ParsePacket(bitStream);
                        p.SubPackets.Add(next);
                    }
                }
                return p;
            }
        }

        public abstract class Packet
        {
            public Packet(int version, int type)
            {
                PacketVersion = version;
                PacketType = type;
            }

            public int PacketVersion { get; }
            public int PacketType { get;  }
            public abstract long Value { get; }
        }

        public class LiteralPacket : Packet
        {
            private readonly long value;

            public LiteralPacket(int packetVersion, long value)
                : base(packetVersion, 4)
            {
                this.value = value;
            }
            public override long Value
            {
                get { return value; }
            }
        }
        public class OperatorPacket : Packet
        {
            public OperatorPacket(int packetVersion, int type)
                : base(packetVersion, type)
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


        
    }

    class BitStream
    {
        public BitStream(string hexStream)
        {
            binaryStream = HexToBinary(hexStream);
        }

        private string HexToBinary(string hex)
        {
            var sb = new StringBuilder();
            foreach (var c in hex)
            {
                sb.Append(Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0'));
            }
            return sb.ToString();
        }

        private string binaryStream;
        public int BitPosition { get; private set; }

        public long ReadNumber(int bits)
        {
            var value = Convert.ToInt64(binaryStream.Substring(BitPosition, bits), 2);
            BitPosition += bits;
            return value;
        }

    }


}
