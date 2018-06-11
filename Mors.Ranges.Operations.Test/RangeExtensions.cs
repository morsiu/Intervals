using System;
using Mors.Ranges.Sequences;

namespace Mors.Ranges.Operations
{
    internal static class RangeExtensions
    {
        public static OpenRange OpenRange(this Range? range)
        {
            return range is Range x
                ? new OpenRange(x.Start, x.End, x.HasOpenStart, x.HasOpenEnd)
                : new OpenRange();
        }

        public static ClosedRange ClosedRange(this Range? range)
        {
            return range is Range x
                ? !x.HasOpenStart && !x.HasOpenEnd
                    ? new ClosedRange(x.Start, x.End)
                    : throw new ArgumentException("The range has open ends.", nameof(range))
                : new ClosedRange();
        }
    }
}