using System;
using System.Globalization;

namespace Mors.Ranges.Test.Support.RangeOperations
{
    public readonly struct AxisPosition : IEquatable<AxisPosition>
    {
        private readonly int _startPosition;

        public AxisPosition(int startPosition)
            => _startPosition = startPosition;

        public AxisVector Subtract(AxisPosition other)
            => new AxisVector(_startPosition - other._startPosition);

        public AxisPosition Add(AxisVector other)
        {
            var otherMagnitude = other.Length().Magnitude();
            return new AxisPosition(
                _startPosition + (other.Left() ? -otherMagnitude : otherMagnitude));
        }

        public bool Equals(AxisPosition other)
            => _startPosition.Equals(other._startPosition);

        public override bool Equals(object obj)
            => obj is AxisPosition && Equals((AxisPosition)obj);

        public override int GetHashCode()
            => _startPosition;

        public override string ToString()
        {
            return _startPosition.ToString(CultureInfo.InvariantCulture);
        }
    }
}