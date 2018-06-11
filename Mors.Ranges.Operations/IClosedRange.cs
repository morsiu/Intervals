// Copyright (C) 2017 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;

namespace Mors.Ranges.Operations
{
    /// <summary>
    /// Defines a closed range.
    /// </summary>
    /// <typeparam name="T">The type of values of the ranges' ends.</typeparam>
    public interface IClosedRange<out T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Gets a value indicating whether the instance represents an empty range.
        /// </summary>
        bool Empty { get; }
        
        /// <summary>
        /// Gets the value of the start end.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The Empty condition is true.
        /// </exception>>
        T Start { get; }
        
        /// <summary>
        /// Gets the value of the ending end.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The Empty condition is true.
        /// </exception>>
        T End { get; }
    }
}