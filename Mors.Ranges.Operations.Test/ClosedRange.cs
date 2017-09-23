// Copyright (C) 2017 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

namespace Mors.Ranges.Operations
{
    internal struct ClosedRange : IClosedRange<int>, IRange<int>
    {
        private IRange<int> _range;

        public ClosedRange(IRange<int> range)
        {
            _range = range;
        }

        public bool IsEmpty => _range.IsEmpty;

        public int Start => _range.Start;

        public int End => _range.End;

        public bool HasOpenStart => _range.HasOpenStart;

        public bool HasOpenEnd => _range.HasOpenEnd;

        public bool Empty => _range.IsEmpty;

        public bool Equals(IRange<int> other)
        {
            return _range.Equals(other);
        }
    }
}
