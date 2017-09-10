using System;
using System.Collections;
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
        /// Returns true if type implements an interface interfaceType,
        ///  returns false if not or interfaceType is not an interface or is self or is null
        /// </summary>
        /// <param name="type"></param>
        /// <param name="interfaceType"></param>
        /// <returns></returns>
        public static bool Implements(this Type type, Type interfaceType)
        {
            return interfaceType != null && interfaceType != type && interfaceType.GetTypeInfo().IsInterface && interfaceType.GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
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

        /// <summary>
        /// Returns true if the type contains the propertyInfo at root level.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static bool ContainsProperty(this Type type, PropertyInfo propertyInfo)
        {
            return type.GetRuntimeProperties().FirstOrDefault(info => info.Equals(propertyInfo)) != null;
        }
        /// <summary>
        /// Returns true if the object contains the propertyInfo at root level.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static bool ContainsProperty(this object instance, PropertyInfo propertyInfo)
        {
            return instance.GetType().GetRuntimeProperties().FirstOrDefault(info => info.Equals(propertyInfo)) != null;
        }

        /// <summary>
        /// Returns properties in instance that matches the predicate
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetFilteredProperties(this object instance, Func<PropertyInfo,bool> predicate)
        {
            return instance.GetType().GetRuntimeProperties().Where(predicate);
        }

        /// <summary>
        /// Returns properties in type that matches the predicate
        /// </summary>
        /// <param name="type"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetFilteredProperties(this Type type, Func<PropertyInfo,bool> predicate)
        {
            return type.GetRuntimeProperties().Where(predicate);
        }
        
        /// <summary>
        /// Return the value of propertyInfo in root's or children's instance property
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static object GetChildrenValue(this PropertyInfo propertyInfo, object instance)
        {
            if (instance.ContainsProperty(propertyInfo))
            {
                return propertyInfo.GetValue(instance);
            }

            foreach (var navigationProperty in instance.GetFilteredProperties(info => !info.PropertyType.IsPrimitive()))
            {
                var targetProperty = navigationProperty
                    .PropertyType
                    .GetRuntimeProperties()
                    .FirstOrDefault(info => info.Equals(propertyInfo));
                if (targetProperty == null) continue;


                var targetInstance = instance.GetType()
                    .GetRuntimeProperty(navigationProperty.Name)
                    .GetValue(instance);



                return !propertyInfo.PropertyType.IsPrimitive() ?
                    GetChildrenValue(propertyInfo, targetInstance)
                    : propertyInfo.GetValue(targetInstance);
            }

            return null;
        }

    }
}
