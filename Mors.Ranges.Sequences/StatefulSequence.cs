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
            IState Next(in Element<TInput> point);

            (bool HasOutput, TOutput Output) Output(in Element<TInput> point);

            void Last();
        }
    }
}
