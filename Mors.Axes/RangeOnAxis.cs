using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Test.Support.RangeOperations
{
    public sealed class RangeOnAxis : IAxisSegment
    {
        private readonly int _endPosition;
        private readonly int _startPosition;

        public RangeOnAxis(int startPosition, int endPosition)
        {
            if (endPosition < startPosition)
                throw new ArgumentException($"The argument {nameof(endPosition)} must be greater than or equal to the argument {nameof(startPosition)}.");
            _endPosition = endPosition;
            _startPosition = startPosition;
        }

        public IEnumerator<AxisPoint> GetEnumerator()
            => Enumerable.Repeat(AxisPoint.InsideRange, _endPosition - _startPosition + 1).GetEnumerator();

        public AxisDistance Length()
            => new AxisDistance(_endPosition - _startPosition);

        public AxisPosition Start()
            => new AxisPosition(_startPosition);

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}