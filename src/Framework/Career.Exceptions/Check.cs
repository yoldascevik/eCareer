using System;
using System.Diagnostics.CodeAnalysis;

namespace Career.Exceptions
{
    public static class Check
    {
        public static void NotNull(object value, [NotNull] string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}