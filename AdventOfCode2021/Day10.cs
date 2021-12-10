using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{

    public class Day10 : ISolver
    {
        public (string, string) ExpectedResult => ("240123", "3260812321");

        public (string, string) Solve(string[] input)
        {
            var scores = input.Select(ParseLine).ToList();
            var syntaxScore = scores.Select(s => s.SyntaxError).Sum();

            var incompleteScores = scores.Where(s => s.SyntaxError == 0 && s.CompletionScore != 0).OrderBy(s => s.CompletionScore).ToList();
            var middleScore = incompleteScores[incompleteScores.Count / 2].CompletionScore;

            return ($"{syntaxScore}", $"{middleScore}");
        }

        private Dictionary<char, int> syntaxScore = new Dictionary<char, int>() { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };
        private Dictionary<char, int> completionScores = new Dictionary<char, int>() { { '(', 1 }, { '[', 2 }, { '{', 3 }, { '<', 4 } };
        private Dictionary<char, char> pairs = new Dictionary<char, char>() { { ')', '(' }, { ']', '[' }, { '}', '{' }, { '>', '<' }};

        public (long SyntaxError, long CompletionScore) ParseLine(string line)
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
                            return (syntaxScore[c],0);
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
            var completionScore = 0L;
            while(stack.TryPop(out char opener))
            {
                completionScore *= 5;
                completionScore += completionScores[opener];
            }
            return (0, completionScore);
        }


    }

}
