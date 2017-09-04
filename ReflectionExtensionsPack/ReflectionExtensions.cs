using System;
using System.Reflection;

namespace ReflectionExtensionsPack
{
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Returns true if type implements an interface T,
        ///  returns false if not or T is not an interface or is self
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool Implements<T>(this Type type)
        {
            return typeof(T) != type && typeof(T).GetTypeInfo().IsInterface && typeof(T).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
        }
    }
}
