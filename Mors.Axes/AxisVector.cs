using System;

namespace Mors.Ranges.Test.Support.RangeOperations
{
    public struct AxisVector
    {
        private readonly int _magnitudeAndDirection;

        public AxisVector(int magnitudeAndDirection)
            => _magnitudeAndDirection = magnitudeAndDirection;

        public AxisDistance Distance()
            => new AxisDistance(Math.Abs(_magnitudeAndDirection));

        public bool Left()
            => _magnitudeAndDirection < 0;

        public bool Zero()
            => _magnitudeAndDirection == 0;
    }
}