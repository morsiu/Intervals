// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;
using Walrus.Ranges.Text;

namespace Walrus.Ranges.Test.Support.RangeOperations.StateMachines
{
    internal struct PointSequencePair
    {
        private PointSequence _sequenceB;
        private PointSequence _sequenceA;

        public PointSequencePair(PointSequence sequenceA, PointSequence sequenceB)
            : this()
        {
            _sequenceA = sequenceA.Pad(sequenceB);
            _sequenceB = sequenceB.Pad(sequenceA);
        }


        public PointSequence Zip(Func<PointTypePair, PointType> zipper)
        {
            return new PointSequence(
                Zip<PointType>(zipper),
                _sequenceA.Offset);
        }

        public IEnumerable<TValue> Zip<TValue>(Func<PointTypePair, TValue> zipper)
        {
            return _sequenceA.Zip(_sequenceB, PointTypePair.Create).Select(zipper);
        }
    }
}
