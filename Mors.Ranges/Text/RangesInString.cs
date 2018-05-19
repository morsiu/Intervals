// Copyright (C) 2018 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Text
{
    public sealed class RangesInString : IEnumerable<IRange<int>>
    {
        private readonly string _string;
        private readonly PointTypeCharacters _characters;

        public RangesInString(string @string, PointTypeCharacters characters)
        {
            _string = @string;
            _characters = characters;
        }

        public IEnumerator<IRange<int>> GetEnumerator()
        {
            return _string
                .Select((x, i) => new Point(_characters.MaybePointType(x) ?? throw new Exception($"Invalid character {x} at index {i}."), new Index(i)))
                .Aggregate(
                    (State: (IState)new Outside(), Results: new List<IRange<int>>()),
                    (x, y) =>
                    {
                        var (state, result) = x.State.Next(y);
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
            public Point(PointType type, Index index)
            {
                Type = type;
                Index = index;
            }

            public PointType Type { get; }
            public Index Index { get; }
            public Position Position => Index.Position();
        }

        public readonly struct Position
        {
            private readonly int _position;
            public Position(int position) => _position = position;
            public Index Index() => new Index(_position + 1);
            public override string ToString() => _position.ToString();
            public static implicit operator int(Position x) => x._position;
        }

        public readonly struct Index
        {
            private readonly int _index;
            public Index(int index) => _index = index;
            public Position Position() => new Position(_index + 1);
            public override string ToString() => _index.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private interface IState
        {
            (IState State, IRange<int> Result) Next(in Point point);

            IRange<int> Last();
        }

        private sealed class Outside : IState
        {
            public IRange<int> Last()
            {
                return null;
            }

            public (IState State, IRange<int> Result) Next(in Point point)
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

            public IRange<int> Last()
            {
                throw new UnexpectedEndOfInputException("OpenStart");
            }

            public (IState State, IRange<int> Result) Next(in Point point)
            {
                switch (point.Type)
                {
                    case PointType.Covered:
                        return (new Inside(_start, true), null);
                    case PointType.ClosedEnd:
                        return (new Outside(), Range.Create<int>(_start, point.Position, true, false));
                    case PointType.OpenEnd:
                        return (new Outside(), Range.Create<int>(_start, point.Position, true, true));
                    default:
                        throw new UnexpectedInputException(point, "OpenStart");
                }
            }
        }

        private sealed class ClosedStart : IState
        {
            private readonly Position _start;

            public ClosedStart(Position start) => _start = start;

            public IRange<int> Last()
            {
                return Range.Create<int>(_start, _start, false, false);
            }

            public (IState State, IRange<int> Result) Next(in Point point)
            {
                switch (point.Type)
                {
                    case PointType.Uncovered:
                        return (new Outside(), Range.Closed<int>(_start, _start));
                    case PointType.Covered:
                        return (new Inside(_start, false), null);
                    case PointType.ClosedEnd:
                        return (new Outside(), Range.Create<int>(_start, point.Position, false, false));
                    case PointType.OpenEnd:
                        return (new Outside(), Range.Create<int>(_start, point.Position, false, true));
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

            public IRange<int> Last()
            {
                throw new UnexpectedEndOfInputException("Inside");
            }

            public (IState State, IRange<int> Result) Next(in Point point)
            {
                switch (point.Type)
                {
                    case PointType.Covered:
                        return (this, null);
                    case PointType.OpenEnd:
                        return (new Outside(), Range.Create<int>(_start, point.Position, _hasOpenStart, true));
                    case PointType.ClosedEnd:
                        return (new Outside(), Range.Create<int>(_start, point.Position, _hasOpenStart, false));
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
                $"Unexpected point type {_point.Type} at index {_point.Index} in state {_stateName}";
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
