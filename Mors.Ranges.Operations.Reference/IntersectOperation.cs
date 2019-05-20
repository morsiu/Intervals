// Copyright (C) 2018 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Collections;
using System.Collections.Generic;
using Mors.Ranges.Sequences;

namespace Mors.Ranges.Operations.Reference
{
    internal sealed class IntersectOperation : IPointSequence
    {
        private static readonly StateTable<PairOfPointTypes, PointType> States =
            new StateTableBuilder<char, char, char>()
            .AssumingHeader('-', '(', '[', '#', '=', ')', ']')
            .AppendRow('-', '-', '-', '-', '-', '-', '-', '-')
            .AppendRow('(', '-', '(', '(', '(', '(', '-', '-')
            .AppendRow('[', '-', '(', '[', '[', '[', '-', '#')
            .AppendRow('#', '-', '(', '[', '#', '=', ')', ']')
            .AppendRow('=', '-', '(', '[', '=', '=', ')', ']')
            .AppendRow(')', '-', '-', '-', ')', ')', ')', ')')
            .AppendRow(']', '-', '-', '#', ']', ']', ')', ']')
            .Build(PointTypeCharacters.PairOfPointTypes, PointTypeCharacters.PointType);

        private readonly ZippedPointSequence _result;

        public IntersectOperation(IPointSequence first, IPointSequence second) =>
            _result =
                new ZippedPointSequence(
                    new AlignedPointSequence(first, second),
                    new AlignedPointSequence(second, first),
                    States.Match);

        public IEnumerator<PointType> GetEnumerator() => _result.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Start => _result.Start;
        public int Length => _result.Length;
    }
}
