// Copyright (C) 2018 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Sequences
{
    public sealed class AlignedPointSequence : IPointSequence
    {
        private readonly IPointSequence _main;
        private readonly IPointSequence _other;

        public AlignedPointSequence(
            IPointSequence main,
            IPointSequence other)
        {
            _main = main;
            _other = other;
        }

        public int Start => Math.Min(_main.Start, _other.Start);

        public int Length =>
            new RightPadding(_main.Start, _main.Length, _other.Start, _other.Length).End
            - new LeftPadding(_main.Start, _other.Start).Start
            + 1;

        public IEnumerator<PointType> GetEnumerator()
        {
            var leftPadding = new LeftPadding(_main.Start, _other.Start);
            foreach (var pointType in Enumerable.Repeat(PointType.Uncovered, leftPadding.Length))
            {
                yield return pointType;
            }
            foreach (var pointType in _main)
            {
                yield return pointType;
            }
            var rightPadding = new RightPadding(_main.Start, _main.Length, _other.Start, _other.Length);
            foreach (var pointType in Enumerable.Repeat(PointType.Uncovered, rightPadding.Length))
            {
                yield return pointType;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}