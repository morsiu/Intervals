using System;
using System.Linq;
using Mors.Ranges.Sequences;

namespace Mors.Ranges
{
    internal sealed class FirstRangeInPointSequence : IRange<int>
    {
        private readonly Lazy<IRange<int>> _range;

        public FirstRangeInPointSequence(IPointSequence pointSequence)
        {
            _range = new Lazy<IRange<int>>(
                () => new RangesInPointSequence(pointSequence)
                    .Select(x => Range.Create<int>(x.Start, x.End, x.HasOpenStart, x.HasOpenEnd))
                    .DefaultIfEmpty(Range.Empty<int>())
                    .SingleOrDefault());
        }

        public override bool Equals(object obj) => obj is IRange<int> other && Equals(other);
        public bool Equals(IRange<int> other) => RangeEqualityComparer<int>.Instance.Equals(_range.Value, other);
        public override int GetHashCode() => RangeEqualityComparer<int>.Instance.GetHashCode(_range.Value);
        public override string ToString() => _range.Value.ToString();

        public bool IsEmpty => _range.Value.IsEmpty;
        public int Start => _range.Value.Start;
        public int End => _range.Value.End;
        public bool HasOpenStart => _range.Value.HasOpenStart;
        public bool HasOpenEnd => _range.Value.HasOpenEnd;
    }
}