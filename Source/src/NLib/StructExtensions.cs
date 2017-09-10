using System;
using System.Globalization;

namespace NLib
{
    public static class StructExtensions
    {
        public static string ToStringInvariant<TStruct>(this TStruct @struct)
            where TStruct : struct
        {
            return Convert.ToString(@struct, CultureInfo.InvariantCulture);
        }

        public static string ToStringInvariant<TStruct>(this TStruct? @struct)
            where TStruct : struct
        {
            return @struct?.ToStringInvariant();
        }
    }
}
