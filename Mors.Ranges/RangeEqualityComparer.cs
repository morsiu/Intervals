// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;

namespace Mors.Ranges
{
    public sealed class RangeEqualityComparer<T> : IEqualityComparer<IRange<T>>
        where T : IComparable<T>
    {
        public static readonly RangeEqualityComparer<T> Instance = new RangeEqualityComparer<T>();

        private RangeEqualityComparer()
        {
        }

        public bool Equals(IRange<T> x, IRange<T> y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }
            if (ReferenceEquals(x, null) ||
                ReferenceEquals(y, null))
            {
                return false;
            }
            if (x.IsEmpty == true && y.IsEmpty == true)
            {
                return true;
            }
            return x.IsEmpty == false
                && y.IsEmpty == false
                && x.HasOpenStart == y.HasOpenStart
                && x.HasOpenEnd == y.HasOpenEnd
                && x.Start.CompareTo(y.Start) == 0
                && x.End.CompareTo(y.End) == 0;
        }

        public int GetHashCode(IRange<T> obj)
        {
            if (obj == null || obj.IsEmpty)
            {
                return 0;
            }
            unchecked
            {
                var hashCode = ReferenceEquals(obj.Start, null) ? 0 : obj.Start.GetHashCode();
                hashCode = (hashCode * 397) ^ (ReferenceEquals(obj.End, null) ? 0 : obj.End.GetHashCode());
                hashCode = (hashCode * 397) ^ obj.HasOpenStart.GetHashCode();
                hashCode = (hashCode * 397) ^ obj.HasOpenEnd.GetHashCode();
                return hashCode;
            }
        }
    }
}
