using System;

namespace Unidays.Interview.Common
{
    public static class Guard
    {
        public static void NotNull(object obj, string objectName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException($"{objectName} cannot be null");
            }
        }
    }
}
