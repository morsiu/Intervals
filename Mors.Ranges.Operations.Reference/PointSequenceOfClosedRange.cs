using Mors.Ranges.Sequences;

namespace Mors.Ranges.Operations.Reference
{
    internal sealed class PointSequenceOfClosedRange<TClosedRange>
        where TClosedRange : IRange<int>, IEmptyRange
    {
        private readonly TClosedRange _range;

        public PointSequenceOfClosedRange(TClosedRange range) => _range = range;

        public IPointSequence Value() =>
            _range.Empty
                ? (IPointSequence)new EmptyPointSequence()
                : new PointSequenceFromRange(_range.Start, _range.End, false, false);
    }
}
