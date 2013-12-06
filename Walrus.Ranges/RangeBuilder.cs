// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;

namespace Walrus.Ranges
{
    internal sealed class RangeBuilder<T>
        where T : IComparable<T>
    {
        private RangeCompletion completion = RangeCompletion.HasNothing;
        private T start;
        private bool hasOpenStart;
        private bool hasOpenEnd;
        private T end;

        public IRange<T> Build()
        {
            if (completion == RangeCompletion.HasNothing)
            {
                return Range.Empty<T>();
            }
            if (completion.HasFlag(RangeCompletion.HasStart | RangeCompletion.HasEnd))
            {
                return Range.Create(start, end, hasOpenStart, hasOpenEnd);
            }
            if (completion == RangeCompletion.HasStart)
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
            start = value;
            hasOpenStart = isOpen;
            completion |= RangeCompletion.HasStart;
        }

        public void SetEnd(T value, bool isOpen)
        {
            end = value;
            hasOpenEnd = isOpen;
            completion |= RangeCompletion.HasEnd;
        }

        public void Clear()
        {
            start = default(T);
            end = default(T);
            completion = RangeCompletion.HasNothing;
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
