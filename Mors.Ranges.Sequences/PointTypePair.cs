// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;

namespace Mors.Ranges.Sequences
{
    public struct PointTypePair : IEquatable<PointTypePair>
    {
        private readonly PointType _pointA;
        private readonly PointType _pointB;

        public PointTypePair(PointType pointA, PointType pointB)
            : this()
        {
            _pointA = pointA;
            _pointB = pointB;
        }

        public static PointTypePair Create(PointType pointA, PointType pointB)
        {
            return new PointTypePair(pointA, pointB);
        }

        public bool Equals(PointTypePair other)
        {
            return _pointA == other._pointA && _pointB == other._pointB;
        }

        public override bool Equals(object obj)
        {
            return obj is PointTypePair && Equals((PointTypePair) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) _pointA * 397) ^ (int) _pointB;
            }
        }

        public static bool operator ==(PointTypePair left, PointTypePair right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PointTypePair left, PointTypePair right)
        {
            return !left.Equals(right);
        }
    }
}
