namespace SIS.HTTP.Common
{
    using System;
    public class CoreValidator
    {
        public static void ThrowIfNull(object obj, string name)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void ThrowIfNullOrEmpty(string str, string name)
        {
            if (String.IsNullOrEmpty(str))
            {
                throw new ArgumentException($"{name} cannot be null or empty.", name);
            }
        }
    }
}
