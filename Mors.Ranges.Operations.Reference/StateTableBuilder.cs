// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Operations.Reference
{
    internal sealed class StateTableBuilder<TRowInput, TColumnInput, TOutput>
    {
        private readonly Dictionary<(TRowInput Row, TColumnInput Column), TOutput> _states = new Dictionary<(TRowInput, TColumnInput), TOutput>();
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
                _states.Add((row, column), output);
                ++index;
            }
            return this;
        }

        public StateTable<TInput, TOutput2> Build<TInput, TOutput2>(
            Func<TRowInput, TColumnInput, TInput> inputMerger,
            Func<TOutput, TOutput2> outputConverter)
        {
            return new StateTable<TInput, TOutput2>(
                _states.ToDictionary(
                    x => inputMerger(x.Key.Row, x.Key.Column),
                    x => outputConverter(x.Value)));
        }
    }
}
