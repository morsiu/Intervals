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
