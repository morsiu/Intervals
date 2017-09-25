using System;

namespace Mors.Ranges.Operations
{
    internal sealed class OpenRangeOperationReturningBool
    {
        public delegate bool Operation(ref OpenRange first, ref OpenRange second);

        private readonly Operation _operation;

        public OpenRangeOperationReturningBool(Operation operation)
        {
            _operation = operation;
        }

        public static implicit operator Func<IRange<int>, IRange<int>, bool>(OpenRangeOperationReturningBool operation)
        {
            return operation.Invoke;
        }

        public bool Invoke(IRange<int> first, IRange<int> second)
        {
            var first_ = new OpenRange(first);
            var second_ = new OpenRange(second);
            return _operation(ref first_, ref second_);
        }
    }
}
