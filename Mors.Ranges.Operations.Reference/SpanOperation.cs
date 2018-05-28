// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using Mors.Ranges.Sequences;

namespace Mors.Ranges.Operations.Reference
{
    public static class SpanOperation
    {
        private static readonly PointTypeCharacters Characters = new PointTypeCharacters('-', '=', 'x', 'o');

        private enum State
        {
            BeforeOrAfter,
            InFirstRange,
            InSecondRange,
            InBothRanges,
            BetweenRanges,
            Invalid
        }

        private static readonly StateTable<(State, PointTypePair), (State, PointType)> States =
            new StateTableBuilder<string, char, string>()
            .AssumingHeader(  '-')
            .AppendRow("Bx", "L=")
            .AppendRow("Bo", "L=")
            .AssumingHeader(  '-',  'x',  'o')
            .AppendRow(" -", " -", "Rx", "Ro")
            .AppendRow(" x", "Lx", "2x", "2x")
            .AppendRow(" o", "Lo", "2x", "2o")
            .AppendRow("L=", "L=", "2=", "2=")
            .AppendRow("Lx", "B=", "R=", "R=")
            .AppendRow("Lo", "B=", "R=", "R=")
            .AppendRow("R=", "L=", "2=", "2=")
            .AppendRow("Rx", "B=", "L=", "L=")
            .AppendRow("Ro", "B=", "L=", "L=")
            .AppendRow("B-", "B=", "R=", "R=")
            .AssumingHeader(  'x',  'o',  '=')
            .AppendRow("L-", "Rx", "Ro", "R=")
            .AppendRow("R-", "Rx", "Ro", "R=")
            .AssumingHeader(  '-',  'x',  'o',  '=')
            .AppendRow("2-", " -", "Rx", "Ro", "R=")
            .AppendRow("2x", " x", " x", " x", "R=")
            .AppendRow("2o", " o", " x", " o", "R=")
            .AppendRow("2=", "L=", "L=", "L=", "2=")
            .Build(StateAndPointTypePair, StateAndPointType);

        public static IRange<int> Calculate(IRange<int> rangeA, IRange<int> rangeB)
        {
            var output =
                rangeA.IsEmpty
                    ? rangeB
                    : rangeB.IsEmpty
                        ? rangeA
                        : RangeOperations.Zip(rangeA, rangeB, State.BeforeOrAfter, States);
            return output;
        }

        private static (State, PointType) StateAndPointType(string stateAndPointTypeCharacters)
        {
            return (ToState(stateAndPointTypeCharacters[0]), Characters.PointType(stateAndPointTypeCharacters[1]));
        }

        private static (State, PointTypePair) StateAndPointTypePair(string stateAndPointTypeCharacters, char pointTypeCharacter)
        {
            return (ToState(stateAndPointTypeCharacters[0]), Characters.PointTypePair(stateAndPointTypeCharacters[1], pointTypeCharacter));
        }

        private static State ToState(char stateCharacter)
        {
            switch (stateCharacter)
            {
                case ' ': return State.BeforeOrAfter;
                case 'L': return State.InFirstRange;
                case 'R': return State.InSecondRange;
                case '2': return State.InBothRanges;
                case 'B': return State.BetweenRanges;
                default: throw new ArgumentOutOfRangeException(nameof(stateCharacter), stateCharacter, "Unknown state character");
            }
        }
    }
}
