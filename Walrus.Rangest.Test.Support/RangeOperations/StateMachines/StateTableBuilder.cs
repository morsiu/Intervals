// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;

namespace Walrus.Ranges.Test.Support.RangeOperations.StateMachines
{
    internal sealed class StateTableBuilder<TRowInput, TColumnInput, TOutput>
    {
        private readonly Dictionary<Input, TOutput> _states = new Dictionary<Input, TOutput>();
        private TColumnInput[] _columns;

        public StateTableBuilder<TRowInput, TColumnInput, TOutput> AssumingHeader(params TColumnInput[] columns)
        {
            _columns = columns;
            return this;
        }

        public StateTableBuilder<TRowInput, TColumnInput, TOutput> AppendRow(TRowInput row, params TOutput[] outputs)
        {
            var index = 0;
            foreach (var output in outputs)
            {
                var column = _columns[index];
                _states.Add(new Input(row, column), output);
                ++index;
            }
            return this;
        }

        public StateTable<TInput, TOutput2> Build<TInput, TOutput2>(
            Func<TRowInput, TColumnInput, TInput> inputMerger,
            Func<TOutput, TOutput2> outputConverter)
        {
            var states = MergeInputsAndConvertOutputs(inputMerger, outputConverter);
            return new StateTable<TInput, TOutput2>(states);
        }

        public StateTable<TInput, TState, TOutput2> Build<TInput, TOutput2, TState>(
            Func<TRowInput, TColumnInput, ValueWithState<TInput, TState>> inputMerger,
            Func<TOutput, ValueWithState<TOutput2, TState>> outputConverter)
        {
            var states = MergeInputsAndConvertOutputs(inputMerger, outputConverter);
            return new StateTable<TInput, TState, TOutput2>(states);
        }

        private Dictionary<TInput, TOutput2> MergeInputsAndConvertOutputs<TInput, TOutput2>(
            Func<TRowInput, TColumnInput, TInput> inputMerger,
            Func<TOutput, TOutput2> outputConverter)
        {
            var states = new Dictionary<TInput, TOutput2>();
            foreach (var state in _states)
            {
                var input = state.Key;
                var output = state.Value;
                var mergedInput = input.Merge(inputMerger);
                var convertedOutput = outputConverter(output);
                states.Add(mergedInput, convertedOutput);
            }
            return states;
        }

        private struct Input
        {
            private readonly TColumnInput _columnInput;
            private readonly TRowInput _rowInput;

            public Input(TRowInput rowInput, TColumnInput columnInput)
                : this()
            {
                _rowInput = rowInput;
                _columnInput = columnInput;
            }

            public TState Merge<TState>(Func<TRowInput, TColumnInput, TState> merger)
            {
                return merger(_rowInput, _columnInput);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (ReferenceEquals(_columnInput, null) ? 0 : (_columnInput.GetHashCode() * 137))
                        ^ (ReferenceEquals(_rowInput, null) ? 0 : _rowInput.GetHashCode());
                }
            }

            public override bool Equals(object obj)
            {
                return obj is Input
                    && Equals((Input)obj);
            }

            private bool Equals(Input other)
            {
                return Equals(other._rowInput, _rowInput) && Equals(other._columnInput, _columnInput);
            }
        }
    }
}
