using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021;


public class Day21 : ISolver
{
    public (string, string) ExpectedResult => ("742257", "93726416205179");
    class Player
    {
        private int zeroBasedPos;
        public Player(int pos)
        {
            zeroBasedPos = pos - 1;
        }
        public long Score { get; private set; }
        public int  Pos { get => zeroBasedPos + 1; }

        public void Advance(int spaces)
        {
            zeroBasedPos += spaces;
            zeroBasedPos = zeroBasedPos % 10;
            Score += Pos;
        }        
    };

    class DeterministicDice
    {
        private int n = 0;

        public int Roll()
        {
            n = n % 100;
            return 1 + n++;
        }

        public int Roll(int count)
        {
            return Enumerable.Range(1, count).Sum(n => Roll());
        }
    }

    public (string Part1, string Part2) Solve(string[] input)
    {
        var players = input.Select(x => x.Split(' ').Last()).Select(n => int.Parse(n)).Select(s => new Player(s)).ToArray();
        var startP1 = players[0].Pos;
        var startP2 = players[1].Pos;
        var rolls = 0;
        var dice = new DeterministicDice();
        var gameOver = false;
        while (!gameOver)
        {
            foreach(var player in players)
            {
                var sum = dice.Roll(3);
                rolls += 3;
                player.Advance(sum);
                if (player.Score >= 1000)
                {
                    gameOver = true;
                    break;
                }
                    
            }
        }
        var loserScore = players.Min(p => p.Score);

        var (p1Wins, p2Wins) = PlayMultiverse(startP1, startP2);
        var part2 = Math.Max(p1Wins, p2Wins);

        return ($"{loserScore * rolls}", $"{part2}");
    }

    // I knew the key to part 2 was memoization, but feared I'd still miss some good optimizations (didn't initially spot that the max score is now only 21!)
    // Lazy me - took inspiration from https://www.reddit.com/user/4HbQ/
    // Much more elegant than I would have written, but at least essentially the same approach
    private Dictionary<(int, int, int, int), (long, long)> memo = new Dictionary<(int, int, int, int), (long, long)>();
    private (long,long) PlayMultiverse(int pos1, int pos2, int score1= 0, int score2= 0)
    {
        var key = (pos1, pos2, score1, score2);
        if (memo.TryGetValue(key, out var v))
        {
            return v;
        }

        // 21 is max score for part 2
        if (score2 >= 21)
        {
            memo[key] = (0, 1);
            return (0, 1);
        }

        var wins1 = 0L;
        var wins2 = 0L;
        // possible values of the three dice 3 (occurs once - 1+1+1), 4 can be (1+1+2, 1+2+1, 2+1+1) etc
        // player 1 and 2 switch around each time
        foreach (var (move, n) in new[] { (3, 1), (4, 3), (5, 6), (6, 7), (7, 6), (8, 3), (9, 1) })
        {
            var newPos1 = (pos1 + move) % 10;
            if (newPos1 == 0) newPos1 = 10;
            // swap players 1 and 2 for the next round
            var (w2, w1) = PlayMultiverse(pos2, newPos1, score2, score1 + newPos1);
            (wins1, wins2) = (wins1 + n * w1, wins2 + n * w2);
        }
        memo[key] = (wins1, wins2);
        return (wins1, wins2);
    }
}
