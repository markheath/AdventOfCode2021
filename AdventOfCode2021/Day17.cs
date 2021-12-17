using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2021
{

    public class Day17 : ISolver
    {
        public (string, string) ExpectedResult => ("2278", "996");

        // took some inspiration from rawling to help me track down some
        // issues with my solution https://www.reddit.com/r/adventofcode/comments/ri9kdq/2021_day_17_solutions/how6df7/?utm_source=reddit&utm_medium=web2x&context=3
        public (string, string) Solve(string[] input)
        {
            var points = Regex.Matches(input[0], @"-?\d+").Select(m => int.Parse(m.Value)).ToArray();
            var topLeft = new Coord(points[0], points[3]);
            var bottomRight = new Coord(points[1], points[2]);

            var xRange = Enumerable.Range((int)Math.Sqrt(topLeft.X), bottomRight.X);
            var yRange = Enumerable.Range(bottomRight.Y, -2 * bottomRight.Y);
            var vectors = xRange.SelectMany(x => yRange.Select(y => new Coord(x, y)));

            var hits = vectors.Select(v => FindMaxY(v,topLeft, bottomRight))
                .Where(maxY => maxY.HasValue).ToArray();

            return ($"{hits.Max()}", $"{hits.Length}");
        }

            
        private int? FindMaxY(Coord vector, Coord regionTopLeft, Coord regionBottomRight)
        {
            Coord pos = (0, 0);
            var maxY = 0;
            // keep going while we still have forward momentum (or are above it) and haven't dropped below bottom right
            while((pos.Y >= regionBottomRight.Y) && (vector.X > 0 || (pos.X >= regionTopLeft.X && pos.X <= regionBottomRight.X)))
            {
                if (pos.X >= regionTopLeft.X && pos.X <= regionBottomRight.X && pos.Y <= regionTopLeft.Y && pos.Y >= regionBottomRight.Y)
                {
                    return maxY;
                }
                pos += vector;
                if (pos.Y > maxY) maxY = pos.Y;
                vector = (vector.X > 0 ? vector.X - 1 : 0, vector.Y - 1);
            }

            return null; // doesn't hit the range
        }
    }
}