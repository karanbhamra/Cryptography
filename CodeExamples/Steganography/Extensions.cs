using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Steganography
{
    public static class Extensions
    {
        public static string EnumerableToString<T>(this IEnumerable<char> letters)
        {
            return new string(letters.ToArray());
        }
    }
}
