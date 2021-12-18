using MoreLinq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2021
{

    // broke my laptop today by spilling tea all over it! and this was a hard one and I quickly used up my alotted hour trying to model a nice data structure.
    // So once again I borrowed inspiration from an elegant Python solution, and tried to turn it into idiomatic C#
    // https://github.com/benediktwerner/AdventOfCode/blob/master/2021/day18/sol.py
    // the tree parsing is my own though
    public class Day18 : ISolver
    {
        public (string, string) ExpectedResult => ("4116", "4638");

        public (string Part1, string Part2) Solve(string[] input)
        {
            var lines = input.Select(s => Parse(new StringReader(s))).ToList();
            /*foreach(var line in lines)
            {
                Console.WriteLine(Describe(line));
            }*/
            var p1 = Magnitude(lines.Aggregate(Add));
            var p2 = lines.Subsets(2).Max(s => Math.Max(Magnitude(Add(s[0], s[1])), Magnitude(Add(s[1],s[0]))));
            // max(magnitude(add(a, b)) for a, b in itertools.permutations(lines, 2)),
            return ($"{p1}", $"{p2}");
        }

        interface INode { }
        record ValueNode(int Value) : INode;
        record PairNode(INode Left, INode Right) : INode;

        private string Describe(INode node)
        {
            if (node is ValueNode v)
            {
                return v.Value.ToString();
            }
            var p = (PairNode)node;
            return $"[{Describe(p.Left)},{Describe(p.Right)}]";
        }

        private INode Parse(TextReader input)
        {
            var c = input.Read();
            if (c == '[')
            {
                var left = Parse(input);
                if (input.Read() != ',') throw new InvalidOperationException();                
                var right = Parse(input);
                if (input.Read() != ']') throw new InvalidOperationException();
                return new PairNode(left, right);
            }
            if (c >= '0' && c <= '9')
            { 
                return new ValueNode(c - '0');
            }
            throw new InvalidOperationException($"Unexpected final char {c}");
        }

        private INode AddLeft(INode x, INode n)
        {
            if (n == null)
                return x;
            if (x is ValueNode v)
                return new ValueNode(v.Value + ((ValueNode)n).Value);
            if (x is PairNode a)
                return new PairNode(AddLeft(a.Left, n), a.Right);
            throw new InvalidOperationException();
        }

        private INode AddRight(INode x, INode n)
        {
            if (n == null)
                return x;
            if (x is ValueNode v)
                return new ValueNode(v.Value + ((ValueNode)n).Value);
            if (x is PairNode a)
                return new PairNode(a.Left, AddRight(a.Right, n));
            throw new InvalidOperationException();
        }

        // n is nesting level
        // returns whether we exploded, the new left, the new current node, and the new right.
        // new left and right will be null if we didn't need to explode
        private (bool, INode, INode, INode) Explode(INode x, int n = 4)
        {
            if (x is ValueNode)
            {
                return (false, null, x, null);
            }
            var p = x as PairNode;

            if (n == 0)
            {
                return (true, p.Left, new ValueNode(0), p.Right);
            }
            //var a = p.Left;
            var b = p.Right;
            var (exp, left, a, right) = Explode(p.Left, n - 1);
            if (exp)
                return (true, left, new PairNode(a, AddLeft(b, right)), null);
            (exp, left, b, right) = Explode(b, n - 1);
            if (exp)
                return (true, null, new PairNode(AddRight(a, left), b), right);
            return (false, null, x, null);
        }


        private (bool, INode) Split(INode x)
        {
            if (x is ValueNode v)
            {
                if (v.Value >= 10)
                {
                    // left is round down, right is round up
                    return (true, new PairNode(new ValueNode(v.Value / 2), new ValueNode((v.Value + 1) / 2)));
                }
                return (false, x);
            }
            var p = x as PairNode;
            var a = p.Left;
            var b = p.Right;
            bool change;
            (change, a) = Split(a);
            if (change)
                return (true, new PairNode(a, b));
            (change, b) = Split(b);
            return (change, new PairNode(a, b));
        }

        private INode Add(INode a, INode b)
        {
            INode x = new PairNode(a, b);
            bool change;
            while (true)
            {
                (change, _, x, _) = Explode(x);
                if (change)
                    continue;
                (change, x) = Split(x);
                if (!change)
                    break;
            }
            return x;
        }


        private long Magnitude(INode x)
        {
            if (x is ValueNode v)
                return v.Value;
            var p = (PairNode)x;
            return 3 * Magnitude(p.Left) + 2 * Magnitude(p.Right);
        }
    }
}