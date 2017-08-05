// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Test.Support.Extensions
{
    internal static class EnumExtensions
    {
        public static IEnumerable<T> EnumerateFlags<T>(this T flagsEnumValue)
            where T : struct
        {
            var value = Convert.ToInt64(flagsEnumValue);
            var values = Enum.GetValues(typeof(T)).Cast<T>().ToArray();
            foreach (var flag in values)
            {
                var flagValue = Convert.ToInt64(flag);
                if (!HasSingleBitSet(flagValue)) continue;
                if ((value & flagValue) == flagValue) yield return flag;
            }
        }

        private static bool HasSingleBitSet(long flagValue)
        {
            if (flagValue == 0) return false;
            return (flagValue & (flagValue - 1)) == 0;
        }
    }
}
