// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Walrus.Ranges
{
    public static class RangeUnionOperations
    {
        /// <summary>
        /// Normalizes sequence of ranges.
        /// This consists of:
        /// - removing empty ranges;
        /// - spanning intersecting ranges into one;
        /// - ordering ranges in increasing Start value.
        /// </summary>
        /// <typeparam name="T">Type of range ends.</typeparam>
        /// <param name="ranges">Sequence of ranges to normalize.</param>
        public static IEnumerable<IRange<T>> Normalize<T>(IEnumerable<IRange<T>> ranges)
            where T : IComparable<T>
        {
            var enumerator = ranges.Where(r => !r.IsEmpty).OrderBy(r => r.Start).GetEnumerator();
            if (!enumerator.MoveNext()) yield break;
            var current = enumerator.Current;
            while (enumerator.MoveNext())
            {
                var next = enumerator.Current;
                if (current.IntersectsWith(next))
                {
                    current = current.Span(next);
                }
                else
                {
                    yield return current;
                    current = next;
                }
            }
            yield return current;
        }
    }
}
