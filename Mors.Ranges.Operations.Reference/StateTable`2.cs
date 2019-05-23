using System;
using System.Collections.Generic;

namespace Mors.Ranges.Operations.Reference
{
    internal sealed class StateTable<TInput, TOutput>
    {
        private readonly IReadOnlyDictionary<TInput, TOutput> _states;

        public StateTable(IReadOnlyDictionary<TInput, TOutput> states)
        {
            _states = states;
        }

        public TOutput Match(TInput input)
        {
            return _states.TryGetValue(input, out var output)
                ? output
                : throw new ArgumentException($"There is no defined output for input {input}");
        }
    }
}
