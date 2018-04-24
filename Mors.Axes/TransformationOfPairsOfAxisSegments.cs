using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Test.Support.RangeOperations
{
    public readonly struct TransformationOfPairsOfAxisSegments<TState, TResult>
        where TState : IAxisOperationState<TState, TResult>
    {
        private readonly IEnumerable<PairOfAxisPoints> _pairsOfPoints;
        private readonly IAxisOperation<TState, TResult> _operation;

        public TransformationOfPairsOfAxisSegments(IEnumerable<PairOfAxisPoints> pairsOfPoints, IAxisOperation<TState, TResult> operation)
        {
            _pairsOfPoints = pairsOfPoints;
            _operation = operation;
        }

        public TResult Result()
        {
            return
                _pairsOfPoints.Aggregate(
                    _operation.Start(),
                    (state, pairOfPoints) => pairOfPoints.NextState<TState, TResult>(state),
                    state => state.Result());
        }
    }
}