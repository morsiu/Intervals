using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Walrus.Ranges.Test.Cases.Generation.Operations.Parsers
{
    internal static class EnumParser
    {
        public static TEnum ToEnum<TEnum>(char digit)
        {
            var numericEnumValue = int.Parse(digit.ToString());
            return (TEnum)Enum.ToObject(typeof(TEnum), numericEnumValue);
        }
    }
}
