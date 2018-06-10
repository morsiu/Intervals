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
            return new StatefulSequence<PointType, Range>(new Outside(), new Position(_sequence.Start), _sequence).GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private sealed class Outside : StatefulSequence<PointType, Range>.IState
        {
            public (bool HasOutput, Range Output) Output(in Element<PointType> input)
            {
                switch (input.Value)
                {
                    case PointType.ClosedStartAndEnd:
                        return (true, new Range(input.Position, input.Position, false, false));
                    default:
                        return (false, default);
                }
            }

            public void Last()
            {
            }

            public StatefulSequence<PointType, Range>.IState Next(in Element<PointType> input)
            {
                switch (input.Value)
                {
                    case PointType.Outside:
                        return this;
                    case PointType.ClosedStart:
                        return new Inside(input.Position, false);
                    case PointType.OpenStart:
                        return new Inside(input.Position, true);
                    case PointType.ClosedStartAndEnd:
                        return new OutsideAfterOnePointRange();
                    default:
                        throw new UnexpectedInputException(input.Value, input.Position, "Outside");
                }
            }
        }
        
        private sealed class OutsideAfterClosedEnd : StatefulSequence<PointType, Range>.IState
        {
            public (bool HasOutput, Range Output) Output(in Element<PointType> input)
            {
                return (false, default);
            }

            public void Last()
            {
            }

            public StatefulSequence<PointType, Range>.IState Next(in Element<PointType> input)
            {
                switch (input.Value)
                {
                    case PointType.Outside:
                        return new Outside();
                    case PointType.ClosedStart:
                        return new Inside(input.Position, false);
                    case PointType.OpenStart:
                        return new Inside(input.Position, true);
                    default:
                        throw new UnexpectedInputException(input.Value, input.Position, "Outside");
                }
            }
        }
        
        private sealed class OutsideAfterOnePointRange : StatefulSequence<PointType, Range>.IState
        {
            public (bool HasOutput, Range Output) Output(in Element<PointType> input)
            {
                return (false, default);
            }

            public void Last()
            {
            }

            public StatefulSequence<PointType, Range>.IState Next(in Element<PointType> input)
            {
                switch (input.Value)
                {
                    case PointType.Outside:
                        return new Outside();
                    case PointType.OpenStart:
                        return new Inside(input.Position, true);
                    default:
                        throw new UnexpectedInputException(input.Value, input.Position, "Outside");
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

            public (bool HasOutput, Range Output) Output(in Element<PointType> input)
            {
                switch (input.Value)
                {
                    case PointType.OpenEnd:
                        return (true, new Range(_start, input.Position, _hasOpenStart, true));
                    case PointType.ClosedEnd:
                        return (true, new Range(_start, input.Position, _hasOpenStart, false));
                    default:
                        return (false, default);
                }
            }

            public void Last()
            {
                throw new UnexpectedEndOfInputException("Inside");
            }

            public StatefulSequence<PointType, Range>.IState Next(in Element<PointType> input)
            {
                switch (input.Value)
                {
                    case PointType.OpenEnd:
                        return new Outside();
                    case PointType.ClosedEnd:
                        return new OutsideAfterClosedEnd();
                    case PointType.Inside:
                        return this;
                    default:
                        throw new UnexpectedInputException(input.Value, input.Position, "Inside");
                }
            }
        }
    }
}
