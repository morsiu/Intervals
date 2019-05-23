using Mors.Ranges.Sequences;

namespace Mors.Ranges.Operations.Reference
{
    internal sealed class PointSequenceOfOpenRange<TOpenRange>
        where TOpenRange : IRange<int>, IOpenRange, IEmptyRange
    {
        private readonly TOpenRange _range;

        public PointSequenceOfOpenRange(TOpenRange range) => _range = range;

        public IPointSequence Value() =>
            _range.Empty
                ? (IPointSequence)new EmptyPointSequence()
                : new PointSequenceFromRange(_range.Start, _range.End, _range.OpenStart, _range.OpenEnd);
    }
}
