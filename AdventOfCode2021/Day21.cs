using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021;


public class Day21 : ISolver
{
    public (string, string) ExpectedResult => ("", "");
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

    class Dice
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
        var rolls = 0;
        var dice = new Dice();
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

        return ($"{loserScore * rolls}", $"");
    }






}
