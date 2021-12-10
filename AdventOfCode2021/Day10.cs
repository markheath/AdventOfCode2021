using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{

    public class Day10 : ISolver
    {
        public (string, string) ExpectedResult => ("240123", "");

        

        public (string, string) Solve(string[] input)
        {
            var syntaxScore = input.Select(ParseLine).Sum();
            return ($"{syntaxScore}", $"");
        }

        private Dictionary<char, int> syntaxScore = new Dictionary<char, int>() { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };
        private Dictionary<char, char> pairs = new Dictionary<char, char>() { { ')', '(' }, { ']', '[' }, { '}', '{' }, { '>', '<' } };

        public int ParseLine(string line)
        {
            var stack = new Stack<char>();
            foreach(var c in line)
            {
                switch(c)
                {
                    case '(':
                    case '<':
                    case '[':
                    case '{':
                        // going one level of nesting deeper
                        stack.Push(c);
                        break;
                    case ')':
                    case '>':
                    case ']':
                    case '}':
                        if (!stack.TryPeek(out char top) || top != pairs[c])
                        {
                            // closing without an opening
                            return syntaxScore[c];
                        }
                        else
                        {
                            stack.TryPop(out char _);
                        }
                        break;
                    default:
                        throw new InvalidOperationException("unexpected char");
                }
            }
            // incomplete
            return 0;
        }


    }

}
