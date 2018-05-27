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
            return _sequence
                .Select((x, i) => new Point(x, new Position(_sequence.Start + i)))
                .Aggregate(
                    (State: (IState)new Outside(), Results: new List<Range>()),
                    (x, y) =>
                    {
                        var(state, result) = x.State.Next(y);
                        if (result != null) x.Results.Add(result);
                        return (state, x.Results);
                    },
                    x =>
                    {
                        var result = x.State.Last();
                        if (result != null) x.Results.Add(result);
                        return x.Results;
                    })
                .GetEnumerator();
        }

        public readonly struct Point
        {
            public Point(PointType type, Position position)
            {
                Type = type;
                Position = position;
            }

            public PointType Type { get; }
            public Position Position { get; }
        }

        public readonly struct Position
        {
            private readonly int _position;
            public Position(int position) => _position = position;
            public override string ToString() => _position.ToString();
            public static implicit operator int(Position x) => x._position;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private interface IState
        {
            (IState State, Range Result) Next(in Point point);

            Range Last();
        }

        private sealed class Outside : IState
        {
            public Range Last()
            {
                return null;
            }

            public (IState State, Range Result) Next(in Point point)
            {
                switch (point.Type)
                {
                    case PointType.Uncovered:
                        return (this, null);
                    case PointType.OpenEnd:
                        return (new OpenStart(point.Position), null);
                    case PointType.ClosedEnd:
                        return (new ClosedStart(point.Position), null);
                    default:
                        throw new UnexpectedInputException(point, "Outside");
                }
            }
        }

        private sealed class OpenStart : IState
        {
            private readonly Position _start;

            public OpenStart(Position start) => _start = start;

            public Range Last()
            {
                throw new UnexpectedEndOfInputException("OpenStart");
            }

            public (IState State, Range Result) Next(in Point point)
            {
                switch (point.Type)
                {
                    case PointType.Covered:
                        return (new Inside(_start, true), null);
                    case PointType.ClosedEnd:
                        return (new Outside(), new Range(_start, point.Position, true, false));
                    case PointType.OpenEnd:
                        return (new Outside(), new Range(_start, point.Position, true, true));
                    default:
                        throw new UnexpectedInputException(point, "OpenStart");
                }
            }
        }

        private sealed class ClosedStart : IState
        {
            private readonly Position _start;

            public ClosedStart(Position start) => _start = start;

            public Range Last()
            {
                return new Range(_start, _start, false, false);
            }

            public (IState State, Range Result) Next(in Point point)
            {
                switch (point.Type)
                {
                    case PointType.Uncovered:
                        return (new Outside(), new Range(_start, _start, false, false));
                    case PointType.Covered:
                        return (new Inside(_start, false), null);
                    case PointType.ClosedEnd:
                        return (new Outside(), new Range(_start, point.Position, false, false));
                    case PointType.OpenEnd:
                        return (new Outside(), new Range(_start, point.Position, false, true));
                    default:
                        throw new UnexpectedInputException(point, "Start");
                }
            }
        }

        private sealed class Inside : IState
        {
            private readonly Position _start;
            private readonly bool _hasOpenStart;

            public Inside(Position start, bool hasOpenStart)
            {
                _start = start;
                _hasOpenStart = hasOpenStart;
            }

            public Range Last()
            {
                throw new UnexpectedEndOfInputException("Inside");
            }

            public (IState State, Range Result) Next(in Point point)
            {
                switch (point.Type)
                {
                    case PointType.Covered:
                        return (this, null);
                    case PointType.OpenEnd:
                        return (new Outside(), new Range(_start, point.Position, _hasOpenStart, true));
                    case PointType.ClosedEnd:
                        return (new Outside(), new Range(_start, point.Position, _hasOpenStart, false));
                    default:
                        throw new UnexpectedInputException(point, "Inside");
                }
            }
        }

        public sealed class UnexpectedInputException : Exception
        {
            private readonly Point _point;
            private readonly string _stateName;

            public UnexpectedInputException(Point point, string stateName)
            {
                _point = point;
                _stateName = stateName;
            }

            public override string Message =>
                $"Unexpected point type {_point.Type} at position {_point.Position} in state {_stateName}";
        }

        public sealed class UnexpectedEndOfInputException : Exception
        {
            private readonly string _stateName;

            public UnexpectedEndOfInputException(string stateName)
            {
                _stateName = stateName;
            }

            public override string Message =>
                $"Unexpected end of input in state {_stateName}";
        }
    }
}
