using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Walrus.Ranges
{
    /// <summary>
    /// Represents union of ranges.
    /// </summary>
    /// <typeparam name="T">Type of range ends values.</typeparam>
    public interface IRangeUnion<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Gets the union ranges in ascending order.
        /// </summary>
        /// <remarks>
        /// The returned ranges do not overlap and are non-empty.
        /// If the union is empty an empty sequence is returned.
        /// </remarks>
        IEnumerable<IRange<T>> Ranges { get; }

        /// <summary>
        /// Gets value indicating whether the union is empty.
        /// </summary>
        bool IsEmpty { get; }
    }
}
