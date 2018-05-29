// Copyright (C) 2018 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Sequences
{
    public sealed class RangesInPointSequence : IEnumerable<Range>
    {
        private readonly IPointSequence _sequence;

        public RangesInPointSequence(IPointSequence sequence)
        {
            _sequence = sequence;
        }

        public IEnumerator<Range> GetEnumerator()
        {
            return new StatefulSequence<PointType, Range>(new Outside(), _sequence).GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private sealed class Outside : StatefulSequence<PointType, Range>.IState
        {
            public void Last()
            {
            }

            public StatefulSequence<PointType, Range>.IStateOutput Next(in Element<PointType> point)
            {
                switch (point.Value)
                {
                    case PointType.Outside:
                        return new StateOutput(this);
                    case PointType.ClosedStart:
                        return new StateOutput(new Inside(point.Position, false));
                    case PointType.OpenStart:
                        return new StateOutput(new Inside(point.Position, true));
                    case PointType.ClosedStartAndEnd:
                        return new StateOutput(new OutsideAfterOnePointRange(), new Range(point.Position, point.Position, false, false));
                    default:
                        throw new UnexpectedInputException(point.Value, point.Position, "Outside");
                }
            }
        }
        
        private sealed class OutsideAfterClosedEnd : StatefulSequence<PointType, Range>.IState
        {
            public void Last()
            {
            }

            public StatefulSequence<PointType, Range>.IStateOutput Next(in Element<PointType> point)
            {
                switch (point.Value)
                {
                    case PointType.Outside:
                        return new StateOutput(new Outside(), null);
                    case PointType.ClosedStart:
                        return new StateOutput(new Inside(point.Position, false), null);
                    case PointType.OpenStart:
                        return new StateOutput(new Inside(point.Position, true), null);
                    default:
                        throw new UnexpectedInputException(point.Value, point.Position, "Outside");
                }
            }
        }
        
        private sealed class OutsideAfterOnePointRange : StatefulSequence<PointType, Range>.IState
        {
            public void Last()
            {
            }

            public StatefulSequence<PointType, Range>.IStateOutput Next(in Element<PointType> point)
            {
                switch (point.Value)
                {
                    case PointType.Outside:
                        return new StateOutput(new Outside(), null);
                    case PointType.OpenStart:
                        return new StateOutput(new Inside(point.Position, true), null);
                    default:
                        throw new UnexpectedInputException(point.Value, point.Position, "Outside");
                }
            }
        }

        private sealed class Inside : StatefulSequence<PointType, Range>.IState
        {
            private readonly Position _start;
            private readonly bool _hasOpenStart;

            public Inside(Position start, bool hasOpenStart)
            {
                _start = start;
                _hasOpenStart = hasOpenStart;
            }

            public void Last()
            {
                throw new UnexpectedEndOfInputException("Inside");
            }

            public StatefulSequence<PointType, Range>.IStateOutput Next(in Element<PointType> point)
            {
                switch (point.Value)
                {
                    case PointType.OpenEnd:
                        return new StateOutput(new Outside(), new Range(_start, point.Position, _hasOpenStart, true));
                    case PointType.ClosedEnd:
                        return new StateOutput(new OutsideAfterClosedEnd(), new Range(_start, point.Position, _hasOpenStart, false));
                    case PointType.Inside:
                        return new StateOutput(this);
                    default:
                        throw new UnexpectedInputException(point.Value, point.Position, "Inside");
                }
            }
        }

        private sealed class StateOutput : StatefulSequence<PointType, Range>.IStateOutput
        {
            public StateOutput(StatefulSequence<PointType, Range>.IState nextState)
                : this(nextState, false, default)
            {
            }

            public StateOutput(StatefulSequence<PointType, Range>.IState nextState, Range output)
                : this(nextState, true, output)
            {
            }

            private StateOutput(StatefulSequence<PointType, Range>.IState nextState, bool hasOutput, Range output)
            {
                NextState = nextState;
                HasOutput = hasOutput;
                Output = output;
            }

            public bool HasOutput { get; }
            public Range Output { get; }
            public StatefulSequence<PointType, Range>.IState NextState { get; }
        }
    }
}
