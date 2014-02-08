// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections;
using System.Collections.Generic;

namespace Walrus.Ranges.Test.Cases.Generation.Options
{
    internal struct RangePairKinds : IEnumerable<Tuple<RangeKind, RangeKind>>
    {
        private readonly Tuple<RangeKind, RangeKind>[] _rangeKindPairs;

        public static RangePairKinds AllNull()
        {
            return new RangePairKinds(
                Tuple.Create(RangeKind.Null, RangeKind.Null),
                Tuple.Create(RangeKind.Null, RangeKind.Empty),
                Tuple.Create(RangeKind.Empty, RangeKind.Null),
                Tuple.Create(RangeKind.Null, RangeKind.NonEmpty),
                Tuple.Create(RangeKind.NonEmpty, RangeKind.Null));
        }

        public static RangePairKinds AllNonNull()
        {
            return new RangePairKinds(
                Tuple.Create(RangeKind.Empty, RangeKind.Empty),
                Tuple.Create(RangeKind.NonEmpty, RangeKind.Empty),
                Tuple.Create(RangeKind.Empty, RangeKind.NonEmpty),
                Tuple.Create(RangeKind.NonEmpty, RangeKind.NonEmpty));
        }

        public static RangePairKinds All()
        {
            return new RangePairKinds(
                Tuple.Create(RangeKind.Empty, RangeKind.Empty),
                Tuple.Create(RangeKind.NonEmpty, RangeKind.Empty),
                Tuple.Create(RangeKind.Empty, RangeKind.NonEmpty),
                Tuple.Create(RangeKind.NonEmpty, RangeKind.NonEmpty),
                Tuple.Create(RangeKind.Null, RangeKind.Null),
                Tuple.Create(RangeKind.Null, RangeKind.Empty),
                Tuple.Create(RangeKind.Empty, RangeKind.Null),
                Tuple.Create(RangeKind.Null, RangeKind.NonEmpty),
                Tuple.Create(RangeKind.NonEmpty, RangeKind.Null));
        }

        public RangePairKinds(params Tuple<RangeKind, RangeKind>[] rangeKindPairs)
        {
            _rangeKindPairs = rangeKindPairs;
        }

        public IEnumerator<Tuple<RangeKind, RangeKind>> GetEnumerator()
        {
            foreach (var rangeKindPair in _rangeKindPairs) yield return rangeKindPair;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
