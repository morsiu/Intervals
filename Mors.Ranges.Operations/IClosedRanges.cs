// Copyright (C) 2017 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;

namespace Mors.Ranges.Operations
{
    /// <summary>
    /// Provides methods to create instances of closed ranges.
    /// </summary>
    /// <typeparam name="T">The type of values of the ranges' ends.</typeparam>
    /// <typeparam name="TRange">The type of the ranges.</typeparam>
    public interface IClosedRanges<in T, out TRange>
        where T : IComparable<T>
        where TRange : IClosedRange<T>
    {
        /// <summary>
        /// Creates a closed range.
        /// </summary>
        /// <param name="start">A value of the range's start end.</param>
        /// <param name="end">A value of the range's ending end.</param>
        /// <returns>Returns a closed range.</returns>
        /// <remarks>
        /// The returned range is never empty.
        /// </remarks>
        TRange NonEmpty(T start, T end);
        
        /// <summary>
        /// Creates an empty closed range.
        /// </summary>
        /// <returns>
        /// Returns an empty closed range.
        /// </returns>
        TRange Empty();
    }
}