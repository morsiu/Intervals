// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;

namespace Walrus.Ranges
{
    /// <summary>
    /// Represents range between two values (its ends).
    /// The ends may or may not be contained within the range.
    /// </summary>
    /// <typeparam name="T">Type of range ends values.</typeparam>
    public interface IRange<T> : IEquatable<IRange<T>>
        where T : IComparable<T>
    {
        /// <summary>
        /// Gets value indicating whether the range is empty.
        /// </summary>
        /// <remarks>
        /// When range is empty its Start, End, HasOpenStart and HasOpenEnd properties throw InvalidOperationException.
        /// </remarks>
        bool IsEmpty { get; }

        /// <summary>
        /// Gets the value of range start (its smaller end).
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when IsEmpty is true.
        /// </exception>
        T Start { get; }

        /// <summary>
        /// Gets the value of range end (its bigger end).
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when IsEmpty is true.
        /// </exception>
        T End { get; }

        /// <summary>
        /// Gets value indicating whether value of Start is contained within range.
        /// </summary>
        /// <exception>
        /// Thrown when IsEmpty is true.
        /// </exception>
        bool HasOpenStart { get; }

        /// <summary>
        /// Gets value indicating whether value of End is contained within range.
        /// </summary>
        /// <exception>
        /// Thrown when IsEmpty is true.
        /// </exception>
        bool HasOpenEnd { get; }
    }
}
