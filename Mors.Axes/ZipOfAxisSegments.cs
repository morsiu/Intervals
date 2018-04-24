using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Test.Support.RangeOperations
{
    public readonly struct ZipOfAxisSegments : IEnumerable<PairOfAxisPoints>
    {
        private readonly IAxisSegment _left;
        private readonly IAxisSegment _right;

        public ZipOfAxisSegments(IAxisSegment left, IAxisSegment right)
        {
            _left = left;
            _right = right;
        }

        public IEnumerator<PairOfAxisPoints> GetEnumerator()
        {
            // TODO zero-length optimization

            var differenceBetweenStarts = _left.Start().Subtract(_right.Start());
            var differenceBetweenLengths = _left.Length().Difference(_right.Length());
            var leftEnumerator = Left(differenceBetweenStarts, differenceBetweenLengths).GetEnumerator();
            var rightEnumerator = Right(differenceBetweenStarts, differenceBetweenLengths).GetEnumerator();
            // Assumption: Left().Distance() == Right().Distance() => leftEnumerator.MoveNext() == rightEnumerator.MoveNext();
            while (leftEnumerator.MoveNext() && rightEnumerator.MoveNext())
            {
                yield return new PairOfAxisPoints(leftEnumerator.Current, rightEnumerator.Current);
            }
        }

        private IAxisSegment Left(AxisVector differenceBetweenStarts, AxisVector differenceBetweenLengths)
        {
            return
                new RightPaddingOfAxisSegment(
                    new LeftPaddingOfAxisSegment(
                        _left,
                        differenceBetweenStarts.Zero() || !differenceBetweenStarts.Left() ? new AxisDistance() : differenceBetweenStarts.Length()),
                    differenceBetweenLengths.Zero() || differenceBetweenLengths.Left() ? new AxisDistance() : differenceBetweenLengths.Length());
        }

        private IAxisSegment Right(AxisVector differenceBetweenStarts, AxisVector differenceBetweenLengths)
        {
            return
                new RightPaddingOfAxisSegment(
                    new LeftPaddingOfAxisSegment(
                        _right,
                        differenceBetweenStarts.Zero() || differenceBetweenStarts.Left() ? new AxisDistance() : differenceBetweenStarts.Length()),
                    differenceBetweenLengths.Zero() || !differenceBetweenLengths.Left() ? new AxisDistance() : differenceBetweenLengths.Length());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}