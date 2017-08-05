// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;

namespace Mors.Ranges
{
    public sealed class RangeBuilder<T>
        where T : IComparable<T>
    {
        private RangeCompletion _completion = RangeCompletion.HasNothing;
        private T _start;
        private bool _hasOpenStart;
        private bool _hasOpenEnd;
        private T _end;

        public IRange<T> Build()
        {
            if (_completion == RangeCompletion.HasNothing)
            {
                return Range.Empty<T>();
            }
            if (_completion == RangeCompletion.HasStart && _completion == RangeCompletion.HasEnd)
            {
                return Range.Create(_start, _end, _hasOpenStart, _hasOpenEnd);
            }
            if (_completion == RangeCompletion.HasStart)
            {
                throw new InvalidOperationException("Cannot create range without having its end set.");
            }
            else
            {
                throw new InvalidOperationException("Cannot create range without having its start set.");
            }
        }

        public void SetStart(T value, bool isOpen)
        {
            _start = value;
            _hasOpenStart = isOpen;
            _completion |= RangeCompletion.HasStart;
        }

        public void SetEnd(T value, bool isOpen)
        {
            _end = value;
            _hasOpenEnd = isOpen;
            _completion |= RangeCompletion.HasEnd;
        }

        public void Clear()
        {
            _start = default(T);
            _end = default(T);
            _completion = RangeCompletion.HasNothing;
        }

        [Flags]
        enum RangeCompletion
        {
            HasNothing = 0,
            HasStart = 1,
            HasEnd = 2
        }
    }
}
