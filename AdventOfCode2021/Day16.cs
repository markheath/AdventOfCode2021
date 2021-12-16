using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{

    public class Day16 : ISolver
    {
        public (string, string) ExpectedResult => ("981", "");

        public (string, string) Solve(string[] input)
        {
            
            return ($"{GetVersionCount(input[0])}", $"");
        }

        public int GetVersionCount(string packetHex)
        {            
            return GetVersionCount(ParsePacket(packetHex));
        }

        private int GetVersionCount(Packet p)
        {
            if (p is ContainerPacket c)
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
                var literalValue = 0;
                var final = false;
                do
                {
                    if (input[startPos] == '0') final = true;
                    var val = Convert.ToInt32(input.Substring(startPos+1, 4), 2);
                    literalValue <<= 4;
                    literalValue |= val;                    
                    startPos += 5;
                } while (!final);
                return (new LiteralPacket() {  PacketType = typeId, PacketVersion = version, Value = literalValue }, startPos);
            }
            else
            {
                var p = new ContainerPacket();
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

        public class Packet
        {
            

            public int PacketVersion { get; set; }
            public int PacketType { get; set; }

        }

        public class LiteralPacket : Packet
        {
            public int Value { get; set; }
        }
        public class ContainerPacket : Packet
        {
            public ContainerPacket()
            {
                SubPackets = new List<Packet>();
            }
            public List<Packet> SubPackets { get; }
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
