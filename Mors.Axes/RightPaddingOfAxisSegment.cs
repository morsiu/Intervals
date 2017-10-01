using System;
using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Test.Support.RangeOperations
{
    public sealed class RightPaddingOfAxisSegment : IAxisSegment
    {
        public RightPaddingOfAxisSegment(IAxisSegment segment, AxisDistance leftPadding)
        {
        }

        public IEnumerator<AxisPoint> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public AxisDistance Length()
        {
            throw new NotImplementedException();
        }

        public AxisPosition Start()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}