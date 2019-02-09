// Copyright (C) 2018 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using Mors.Ranges.Generation;

namespace Mors.Ranges.Operations
{
    internal readonly struct OpenRanges
        : IOpenRanges<int, OpenRange>,
        IEmptyRanges<OpenRange>,
        Generation.IOpenRanges<OpenRange, (OpenRange, OpenRange)>
    {
        public OpenRange Empty() => new OpenRange();

        public (OpenRange, OpenRange) Pair(OpenRange first, OpenRange second) => (first, second);

        public OpenRange Range(int start, int end, bool isStartOpen, bool isEndOpen) =>
            new OpenRange(start, end, isStartOpen, isEndOpen);

        OpenRange IEmptyRanges<OpenRange>.Empty()
        {
            return new OpenRange();
        }

        OpenRange IOpenRanges<int, OpenRange>.Range(int start, int end, bool openStart, bool openEnd)
        {
            return new OpenRange(start, end, openStart, openEnd);
        }
    }
}
