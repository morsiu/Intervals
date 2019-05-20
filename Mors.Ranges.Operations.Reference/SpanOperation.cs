// Copyright (C) 2018 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mors.Ranges.Sequences;

namespace Mors.Ranges.Operations.Reference
{
    internal sealed class SpanOperation : IPointSequence
    {
        private readonly IPointSequence _first;
        private readonly IPointSequence _second;

        public SpanOperation(IPointSequence first, IPointSequence second)
        {
            _first = new AlignedPointSequence(first, second);
            _second = new AlignedPointSequence(second, first);
        }

        public IEnumerator<PointType> GetEnumerator()
        {
            Range? output = default;
            var ranges =
                new StatefulSequence<PairOfPointTypes, Range>(
                    new Outside(),
                    new Position(Start),
                    _first.Zip(_second, (x, y) => new PairOfPointTypes(x, y)));
            foreach (var range in ranges)
            {
                output = output.HasValue ? Span(output.Value, range) : range;
            }

            return new AlignedPointSequence(
                    output.HasValue
                        ? new PointSequenceFromRange(
                            output.Value.Start,
                            output.Value.End,
                            output.Value.HasOpenStart,
                            output.Value.HasOpenEnd)
                        : (IPointSequence) new EmptyPointSequence(),
                    _first)
                .GetEnumerator();
            
            Range Span(in Range first, in Range second) =>
                new Range(
                    Math.Min(first.Start, second.Start),
                    Math.Max(first.End, second.End),
                    first.Start == second.Start
                        ? first.HasOpenStart && second.HasOpenStart
                        : first.Start < second.Start
                            ? first.HasOpenStart
                            : second.HasOpenStart,
                    first.End == second.End
                        ? first.HasOpenEnd && second.HasOpenEnd
                        : first.End > second.End
                            ? first.HasOpenEnd
                            : second.HasOpenEnd);
        }

        private sealed class Outside : StatefulSequence<PairOfPointTypes, Range>.IState
        {
            public StatefulSequence<PairOfPointTypes, Range>.IState Next(in Element<PairOfPointTypes> input)
            {
                if (input.Value.LeftOrRightIs(PointType.ClosedStart, PointType.OpenStart))
                {
                    return new Inside(new RangeEnd(input));
                }
                return this;
            }

            public (bool HasOutput, Range Output) Output(in Element<PairOfPointTypes> input)
            {
                if ((input.Value.LeftIs(PointType.ClosedStartAndEnd) && input.Value.RightIs(PointType.ClosedStartAndEnd, PointType.Outside))
                    || (input.Value.LeftIs(PointType.Outside, PointType.ClosedStartAndEnd) && input.Value.RightIs(PointType.ClosedStartAndEnd)))
                {
                    return (true, new Range(input.Position, input.Position, false, false));
                }
                return (false, default);
            }

            public void Last()
            {
            }
        }

        private sealed class Inside : StatefulSequence<PairOfPointTypes, Range>.IState
        {
            private readonly RangeEnd _start;

            public Inside(RangeEnd start)
            {
                _start = start;
            }
            
            public StatefulSequence<PairOfPointTypes, Range>.IState Next(in Element<PairOfPointTypes> input)
            {
                if (input.Value.LeftIs(PointType.ClosedEnd, PointType.OpenEnd)
                    && input.Value.RightIs(PointType.ClosedEnd, PointType.OpenEnd, PointType.Outside))
                {
                    return new Outside();
                }
                if (input.Value.LeftIs(PointType.ClosedEnd, PointType.OpenEnd, PointType.Outside)
                    && input.Value.RightIs(PointType.ClosedEnd, PointType.OpenEnd))
                {
                    return new Outside();
                }
                return this;
            }

            public (bool HasOutput, Range Output) Output(in Element<PairOfPointTypes> input)
            {
                if ((input.Value.LeftIs(PointType.ClosedEnd, PointType.OpenEnd) && input.Value.RightIs(PointType.ClosedEnd, PointType.OpenEnd, PointType.Outside))
                    || (input.Value.LeftIs(PointType.ClosedEnd, PointType.OpenEnd, PointType.Outside) && input.Value.RightIs(PointType.ClosedEnd, PointType.OpenEnd)))
                {
                    return (
                        true,
                        _start.Range(new RangeEnd(input)));
                }
                return (false, default);
            }

            public void Last()
            {
                throw new UnexpectedEndOfInputException("Inside");
            }
        }

        private readonly struct RangeEnd
        {
            private readonly Position _position;
            private readonly bool _isOpen;

            public RangeEnd(in Element<PairOfPointTypes> element)
                : this(
                    element.Position,
                    !element.Value.LeftOrRightIs(PointType.ClosedStart) &&
                    !element.Value.LeftOrRightIs(PointType.ClosedEnd))
            {
            }

            private RangeEnd(in Position position, bool isOpen)
            {
                _position = position;
                _isOpen = isOpen;
            }

            public Range Range(RangeEnd end)
            {
                return new Range(
                    _position,
                    end._position,
                    _isOpen,
                    end._isOpen);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Start => _first.Start;
        public int Length => _first.Length;
    }
}
