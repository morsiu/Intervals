using System.Collections.Generic;

namespace Mors.Ranges.Test.Support.RangeOperations
{
    public interface IAxisSegment : IEnumerable<AxisPoint>
    {
        AxisPosition Start();

        AxisDistance Length();
    }
}