using System;
using System.Collections.Generic;
using System.Text;

namespace RyanJuan.Hestia
{
    internal class Error
    {
        internal static ArgumentNullException ArgumentNull(
            string name)
        {
            return new ArgumentNullException(name);
        }

        internal static ArgumentOutOfRangeException ArgumentOutOfRange(
            string name,
            string message,
            object actualValue = null)
        {
            if (actualValue is null)
            {
                return new ArgumentOutOfRangeException(name, message);
            }
            else
            {
                return new ArgumentOutOfRangeException(name, actualValue, message);
            }
        }
    }
}
