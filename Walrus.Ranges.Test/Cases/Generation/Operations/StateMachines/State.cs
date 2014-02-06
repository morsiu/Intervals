// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using Walrus.Ranges.Text;

namespace Walrus.Ranges.Test.Cases.Generation.Operations.StateMachines
{
    internal struct State
    {
        private PointType _inputA;
        private PointType _inputB;
        private PointType _output;

        public State(PointType inputA, PointType inputB, PointType output)
            : this()
        {
            _inputA = inputA;
            _inputB = inputB;
            _output = output;
        }

        public PointType Output { get { return _output; } }
        
        public bool Matches(PointType pointA, PointType pointB)
        {
            return pointA == _inputA && pointB == _inputB;
        }

        public override bool Equals(object obj)
        {
            return obj is State
                && Equals((State)obj);
        }

        private bool Equals(State state)
        {
            return _inputA == state._inputA && _inputB == state._inputB;
        }

        public override int GetHashCode()
        {
            return unchecked((_inputA.GetHashCode() * 137) ^ _inputB.GetHashCode());
        }
    }
}
