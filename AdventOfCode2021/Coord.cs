using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode2021
{
    public struct Coord : IEnumerable<int>, IEquatable<Coord>
    {
        private readonly int x;
        private readonly int y;
        public int X { get => x; }
        public int Y { get => y; }

        public int this[int index]
        {
            get { return index == 0 ? x : y; }
        }

        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static implicit operator (int, int)(Coord c) => (c.x, c.y);
        public static implicit operator Coord((int X, int Y) c) => new Coord(c.X, c.Y);

        public void Deconstruct(out int x, out int y)
        {
            x = this.x;
            y = this.y;
        }

        public static Coord operator +(Coord a, Coord b)
        {
            return new Coord(a.x + b.x, a.y + b.y);
        }

        public override bool Equals(object other) =>
            other is Coord c
                && c.x.Equals(x)
                && c.y.Equals(y);

        // Implement IEquatable<T> https://stackoverflow.com/a/8952026/7532
        public bool Equals([AllowNull] Coord other) => x == other.x && y == other.y;


        public override int GetHashCode()
        //            => HashHelpers.Combine(HashHelpers.Combine(HashHelpers.Combine(HashHelpers.RandomSeed, x), y), z);
        {
            // based on Jon Skeet - hashcode of an int is just its value
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 29 + x;
                hash = hash * 29 + y;
                return hash;
            }
        }

        public override string ToString() => $"({x},{y})";

        public IEnumerator<int> GetEnumerator()
        {
            yield return x;
            yield return y;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}
