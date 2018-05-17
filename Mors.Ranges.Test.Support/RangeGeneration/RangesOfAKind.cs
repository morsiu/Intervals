using System;
using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Test.Support.RangeGeneration
{
    internal sealed class RangesOfAKind : IEnumerable<IRange<int>>
    {
        private readonly RangeKind _rangeKind;

        public RangesOfAKind(RangeKind rangeKind)
        {
            _rangeKind = rangeKind;
        }

        public IEnumerator<IRange<int>> GetEnumerator()
        {
            switch (_rangeKind)
            {
                case RangeKind.NonEmpty:
                    return new NonEmptyRanges().GetEnumerator();
                case RangeKind.Empty:
                    return new EmptyRanges().GetEnumerator();
                case RangeKind.Null:
                    return new NullRanges().GetEnumerator();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
