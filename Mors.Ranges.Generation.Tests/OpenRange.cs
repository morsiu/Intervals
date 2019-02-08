// Copyright (C) 2019 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

namespace Mors.Ranges.Generation.Tests
{
    internal readonly struct OpenRange
    {
        private readonly Point _start;
        private readonly Point _end;
        private readonly bool _nonEmpty;

        public OpenRange(int start, int end, bool isStartOpen, bool isEndOpen)
        {
            _nonEmpty = true;
            _start = new Point(isStartOpen, start);
            _end = new Point(isEndOpen, end);
        }

        public override string ToString() =>
            _nonEmpty
                ? $"{_start.ToString(isStartPoint: true)}, {_end.ToString(isStartPoint: false)}"
                : "∅";

        private readonly struct Point
        {
            public Point(bool isOpen, int value) { _isOpen = isOpen; _value = value; }
            private readonly bool _isOpen;
            private readonly int _value;
            public string ToString(bool isStartPoint) =>
                isStartPoint
                    ? $"{(_isOpen ? '(' : '[')}{_value}"
                    : $"{_value}{(_isOpen ? ')' : ']')}";
        }
    }
}
