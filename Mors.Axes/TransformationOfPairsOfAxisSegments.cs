using System.Collections.Generic;

namespace Mors.Ranges.Test.Support.RangeOperations
{
    public struct TransformationOfPairsOfAxisSegments<TState, TResult>
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