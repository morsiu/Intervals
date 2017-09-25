using System;

namespace Mors.Ranges.Operations
{
    internal sealed class OpenRangeOperationReturningRange
    {
        public delegate void Operation(ref OpenRange first, ref OpenRange second, out OpenRange result);

        private readonly Operation _operation;

        public OpenRangeOperationReturningRange(Operation operation)
        {
            _operation = operation;
        }

        public static implicit operator Func<IRange<int>, IRange<int>, IRange<int>>(OpenRangeOperationReturningRange operation)
        {
            return operation.Invoke;
        }

        public IRange<int> Invoke(IRange<int> first, IRange<int> second)
        {
            var first_ = new OpenRange(first);
            var second_ = new OpenRange(second);
            _operation(ref first_, ref second_, out var result);
            return result;
        }
    }
}
