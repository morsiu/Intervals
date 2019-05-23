using System.Collections.Generic;

namespace Mors.Ranges.Sequences
{
    public interface IPointSequence : IEnumerable<PointType>
    {
        int Start { get; }

        int Length { get; }
    }
}