// Copyright (C) 2019 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Sequences
{
    public sealed class StatefulSequence<TInput, TOutput> : IEnumerable<TOutput>
    {
        private readonly IState _initialState;
        private readonly Position _initialPosition;
        private readonly IEnumerable<TInput> _inputs;

        public StatefulSequence(IState initialState, Position initialPosition, IEnumerable<TInput> inputs)
        {
            _initialState = initialState;
            _initialPosition = initialPosition;
            _inputs = inputs;
        }

        public IEnumerator<TOutput> GetEnumerator()
        {
            var state = _initialState;
            var position = _initialPosition;
            foreach (var input in _inputs)
            {
                var element = new Element<TInput>(input, position);
                var (hasOutput, output) = state.Output(element);
                if (hasOutput)
                {
                    yield return output;
                }
                position = position.Next();
                state = state.Next(element);
            }
            state.Last();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public interface IState
        {
            IState Next(in Element<TInput> input);

            (bool HasOutput, TOutput Output) Output(in Element<TInput> input);

            void Last();
        }
    }
}
