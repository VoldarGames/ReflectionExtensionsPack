using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReflectionExtensionsPack
{
    public static class ReflectionExtensions
    {
        public static readonly ReflectionEnvironment ReflectionEnvironment = new ReflectionEnvironment();

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

        /// <summary>
        /// Returns true if type is primitive such as int,float... or if it is contained in primitiveLists. 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="useStaticPrimitiveList">the type will be searched inside primitive list</param>
        /// <param name="customPrimitiveTypes">the type will be searched inside this custom primitive list</param>
        /// <returns></returns>
        public static bool IsPrimitive(this Type type, bool useStaticPrimitiveList = false, IEnumerable<Type> customPrimitiveTypes = null)
        {
            if (useStaticPrimitiveList && customPrimitiveTypes != null)
            {
                return type.GetTypeInfo().IsPrimitive || ReflectionEnvironment.PrimitiveList.Contains(type) || customPrimitiveTypes.Contains(type); 
            }

            if (useStaticPrimitiveList)
            {
                return type.GetTypeInfo().IsPrimitive || ReflectionEnvironment.PrimitiveList.Contains(type);
            }

            if (customPrimitiveTypes != null)
            {
                return type.GetTypeInfo().IsPrimitive || customPrimitiveTypes.Contains(type);
            }
            return type.GetTypeInfo().IsPrimitive;
        }
    }
}
