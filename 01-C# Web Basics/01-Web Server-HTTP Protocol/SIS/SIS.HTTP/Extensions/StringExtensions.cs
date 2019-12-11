using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIS.HTTP.Extensions
{
    public static class StringExtensions
    {
        public static string Capitalize(this string str)
        {
            return Char.ToUpper(str[0]) + str.Skip(1).ToString().ToLower();
        }
    }
}
