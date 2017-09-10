using System;
using System.Collections.Generic;

namespace ReflectionExtensionsPack
{
    public class ReflectionEnvironment
    {
        public readonly IList<Type> PrimitiveList = new List<Type>();

        /// <summary>
        /// Adds a type T to primitive list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void AddPrimitive<T>()
        {
            if (!PrimitiveList.Contains(typeof(T)))
                PrimitiveList.Add(typeof(T));
        }

        /// <summary>
        /// Removes T type from primitive list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void RemovePrimitive<T>()
        {
            PrimitiveList.Remove(typeof(T));
        }
    }
}