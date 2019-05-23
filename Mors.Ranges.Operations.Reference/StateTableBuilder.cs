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
