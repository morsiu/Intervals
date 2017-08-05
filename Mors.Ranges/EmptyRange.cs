// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;

namespace Mors.Ranges
{
    internal sealed class EmptyRange<T> : IRange<T>
        where T : IComparable<T>
    {
        public static readonly EmptyRange<T> Value = new EmptyRange<T>();

        private EmptyRange()
        {
        }

        public bool IsEmpty => true;

        public T Start => throw new InvalidOperationException();

        public T End => throw new InvalidOperationException();

        public bool HasOpenStart => throw new InvalidOperationException();

        public bool HasOpenEnd => throw new InvalidOperationException();

        public bool Equals(IRange<T> other)
        {
            return RangeEqualityComparer<T>.Instance.Equals(this, other);
        }

        public override bool Equals(object obj)
        {
            return RangeEqualityComparer<T>.Instance.Equals(this, obj as IRange<T>);
        }

        public override int GetHashCode()
        {
            return RangeEqualityComparer<T>.Instance.GetHashCode(this);
        }

        public override string ToString()
        {
            return "empty range";
        }
    }
}
