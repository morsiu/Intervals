// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

namespace Walrus.Ranges.Test.Support.RangeGeneration
{
    public struct RangePair
    {
        private readonly IRange<int> _rangeA;
        private readonly IRange<int> _rangeB;

        public RangePair(IRange<int> rangeA, IRange<int> rangeB)
            : this()
        {
            _rangeA = rangeA;
            _rangeB = rangeB;
        }

        public IRange<int> RangeA { get { return _rangeA; } }

        public IRange<int> RangeB { get { return _rangeB; } }

        public override string ToString()
        {
            return string.Format(
                "Range A: {0}; Range B: {1}",
                RangeA,
                RangeB);
        }
    }
}
