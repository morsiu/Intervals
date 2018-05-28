using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Sequences
{
    public sealed class PaddedPointSequence : IPointSequence
    {
        private readonly IPointSequence _sequence;
        private readonly int _lengthOfLeftPadding;
        private readonly int _lengthOfRightPadding;

        public PaddedPointSequence(IPointSequence sequence, int lengthOfLeftPadding, int lengthOfRightPadding)
        {
            _sequence = sequence;
            _lengthOfLeftPadding = lengthOfLeftPadding;
            _lengthOfRightPadding = lengthOfRightPadding;
        }

        public int Start => _sequence.Start - _lengthOfLeftPadding;

        public int Length => _lengthOfLeftPadding + _sequence.Length + _lengthOfRightPadding;

        public IEnumerator<PointType> GetEnumerator()
        {
            return Enumerable.Repeat(PointType.Uncovered, _lengthOfLeftPadding)
                .Concat(_sequence)
                .Concat(Enumerable.Repeat(PointType.Uncovered, _lengthOfRightPadding))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
