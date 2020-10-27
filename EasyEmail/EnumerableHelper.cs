using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyEmail
{
    public static class EnumerableHelper
    {
        public static string EnumerableToString(this IEnumerable<string> ToBoxs)
        {
           return  string.Join(",", ToBoxs);
        }
        public static IEnumerable<string> StringToEnumerable(this string ToBoxString)
        {
            return ToBoxString.Split(',').ToList<string>();
        }
    }
}
