using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Sequences
{
    public sealed class StatefulSequence<TInput, TOutput> : IEnumerable<TOutput>
    {
        private readonly IState _initialState;
        private readonly IEnumerable<TInput> _inputs;

        public StatefulSequence(IState initialState, IEnumerable<TInput> inputs)
        {
            _initialState = initialState;
            _inputs = inputs;
        }

        public IEnumerator<TOutput> GetEnumerator()
        {
            var state = _initialState;
            var position = new Position(1);
            foreach (var input in _inputs)
            {
                var stateOutput = state.Next(new Element<TInput>(input, position));
                if (stateOutput.HasOutput)
                {
                    yield return stateOutput.Output;
                }
                position = position.Next();
                state = stateOutput.NextState;
            }
            state.Last();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public interface IState
        {
            IStateOutput Next(in Element<TInput> input);

            void Last();
        }

        public interface IStateOutput
        {
            bool HasOutput { get; }
            TOutput Output { get; }
            IState NextState { get; }
        }
    }
}
