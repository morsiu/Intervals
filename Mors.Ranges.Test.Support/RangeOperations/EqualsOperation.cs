// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using Mors.Ranges.Test.Support.RangeOperations.StateMachines;
using Mors.Ranges.Text;

namespace Mors.Ranges.Test.Support.RangeOperations
{
    public static class EqualsOperation
    {
        private static readonly PointTypeCharacters PointTypeCharacters = new PointTypeCharacters('-', '=', 'x', 'o');
        private static readonly BoolCharacters BoolCharacters = new BoolCharacters('t', 'f');

        private static readonly StateTable<PointTypePair, bool> States =
            new StateTableBuilder<char, char, char>()
            .AssumingHeader('=', 'x', 'o', '-')
            .AppendRow('=', 't', 'f', 'f', 'f')
            .AppendRow('x', 'f', 't', 'f', 'f')
            .AppendRow('o', 'f', 'f', 't', 'f')
            .AppendRow('-', 'f', 'f', 'f', 't')
            .Build(PointTypeCharacters.PointTypePair, BoolCharacters.Bool);

        public static bool Calculate(IRange<int> rangeA, IRange<int> rangeB)
        {
            if (rangeA == null || rangeB == null) return rangeA == null && rangeB == null;
            var notEquals = RangeOperations.Any(rangeA, rangeB, output => output == false, States);
            return !notEquals;
        }
    }
}
