// Copyright (C) 2017 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;

namespace Mors.Ranges.Operations
{
    /// <summary>
    /// Defines an open range.
    /// </summary>
    /// <remarks>
    /// An open range is defined as a set of values between two ends.
    /// Each of the two ends may, or may not, belong to the set.
    /// </remarks>
    /// <typeparam name="T">The type of values of open ranges' ends.</typeparam>
    public interface IOpenRange<out T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Gets the value of start end.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The Empty condition is true.
        /// </exception>>
        T Start { get; }
        
        /// <summary>
        /// Gets the value of ending end.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The Empty condition is true.
        /// </exception>
        T End { get; }
        
        /// <summary>
        /// Gets a value indicating whether the instance represents an empty range. 
        /// </summary>
        bool Empty { get; }
        
        /// <summary>
        /// Gets a value indicating whether the start end of the range belongs to the range.
        /// </summary>
        bool OpenStart { get; }
        
        /// <summary>
        /// Gets a value indicating whether the ending end of the range belongs to the range.
        /// </summary>
        bool OpenEnd { get; }
    }
}