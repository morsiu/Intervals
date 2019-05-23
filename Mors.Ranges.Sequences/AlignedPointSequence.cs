using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Sequences
{
    public sealed class AlignedPointSequence : IPointSequence
    {
        private readonly IPointSequence _main;
        private readonly IPointSequence _other;

        public AlignedPointSequence(
            IPointSequence main,
            IPointSequence other)
        {
            _main = main;
            _other = other;
        }

        public int Start => Math.Min(_main.Start, _other.Start);

        public int Length =>
            LeftPadding().Length
            + _main.Length
            + RightPadding().Length;

        public IEnumerator<PointType> GetEnumerator()
        {
            var leftPadding = LeftPadding();
            foreach (var pointType in Enumerable.Repeat(PointType.Outside, leftPadding.Length))
            {
                yield return pointType;
            }
            foreach (var pointType in _main)
            {
                yield return pointType;
            }
            var rightPadding = RightPadding();
            foreach (var pointType in Enumerable.Repeat(PointType.Outside, rightPadding.Length))
            {
                yield return pointType;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private RightPadding RightPadding() =>
            new RightPadding(_main.Start, _main.Length, _other.Start, _other.Length);

        private LeftPadding LeftPadding() =>
            new LeftPadding(_main.Start, _other.Start);
    }
}