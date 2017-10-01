using System;

namespace Mors.Ranges.Test.Support.RangeOperations
{
    public struct AxisDistance
    {
        private readonly int _magnitude;

        public AxisDistance(int magnitude)
        {
            if (magnitude < 0)
                throw new ArgumentOutOfRangeException(nameof(magnitude), $"The argument {nameof(magnitude)} must be greater than or equal to zero.");
            _magnitude = magnitude;
        }

        public int Magnitude() => _magnitude;

        public AxisVector Difference(AxisDistance other)
            => new AxisVector(_magnitude - other._magnitude);

        public bool Zero()
            => _magnitude == 0;
    }
}