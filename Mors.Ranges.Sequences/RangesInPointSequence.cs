using System.Collections;
using System.Collections.Generic;

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
