// Copyright (C) 2018 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Collections.Generic;
using System.Linq;
using Mors.Ranges.Sequences;

namespace Mors.Ranges.Operations.Reference
{
    public sealed class IntersectsWithOperation
    {
        private static readonly BoolCharacters BoolCharacters = new BoolCharacters('#', ' ');

        private static readonly StateTable<PairOfPointTypes, bool> States =
            new StateTableBuilder<char, char, char>()
            .AssumingHeader('-', '(', '[', '#', '=', ')', ']')
            .AppendRow('-', ' ', ' ', ' ', ' ', ' ', ' ', ' ')
            .AppendRow('(', ' ', ' ', ' ', ' ', ' ', ' ', ' ')
            .AppendRow('[', ' ', ' ', '#', '#', '#', ' ', '#')
            .AppendRow('#', ' ', ' ', '#', '#', '#', ' ', '#')
            .AppendRow('=', ' ', ' ', '#', '#', '#', ' ', '#')
            .AppendRow(')', ' ', ' ', ' ', ' ', ' ', ' ', ' ')
            .AppendRow(']', ' ', ' ', '#', '#', '#', ' ', '#')
            .Build(PointTypeCharacters.PairOfPointTypes, BoolCharacters.Bool);

        private readonly IEnumerable<bool> _result;

        public IntersectsWithOperation(IPointSequence first, IPointSequence second) =>
            _result =
                new ZipOfPointSequences<bool>(
                    new AlignedPointSequence(first, second),
                    new AlignedPointSequence(second, first),
                    States.Match);

        public bool Result() => _result.Any(x => x);
    }
}
