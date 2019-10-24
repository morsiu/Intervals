using System;

namespace Mors.Ranges.Inequations.Tests
{
    public readonly struct Point : IComparable<Point>
    {
        private readonly int _value;

        public Point(int value) => _value = value;

        public static implicit operator Point(int value) => new Point(value);

        public static Point Maximum => int.MaxValue;

        public static Point Minimum => int.MinValue;

        public int CompareTo(Point other) => _value.CompareTo(other._value);

        public override bool Equals(object obj) =>
            obj is Point point && _value == point._value;

        public override int GetHashCode() =>
            HashCode.Combine(_value);

        public (bool HasValue, Point Value) Next() =>
            _value < int.MaxValue
                ? (true, new Point(_value + 1))
                : (false, default);

        public (bool HasValue, Point Value) Previous() =>
            _value > int.MinValue
                ? (true, new Point(_value - 1))
                : (false, default);

        public override string ToString() =>
            _value == int.MaxValue
                ? "max"
                : _value == int.MinValue
                    ? "min"
                    : _value.ToString();
    }
}