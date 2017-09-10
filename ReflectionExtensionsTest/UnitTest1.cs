using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReflectionExtensionsPack;
using ReflectionExtensionsTest.TestDomain;
using ReflectionExtensionsTest.TestDomain.Interfaces;

namespace ReflectionExtensionsTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ImplementsMethodOkTest()
        {
            Assert.IsTrue(typeof(Dog).Implements<IKillable>());
            Assert.IsFalse(typeof(IKillable).Implements<IKillable>());
            Assert.IsFalse(typeof(Dog).Implements<Dog>());
            Assert.IsFalse(typeof(Dog).Implements<Animal>());
            Assert.IsFalse(typeof(Dog).Implements<int>());
        }

        [TestMethod]
        public void IsPrimitiveMethodOkTest()
        {
            Assert.IsTrue(typeof(int).IsPrimitive());    //Int is primitive type.

            Assert.IsFalse(typeof(string).IsPrimitive());  //string is not a primitive type
            Assert.IsFalse(typeof(string).IsPrimitive(useStaticPrimitiveList: true)); //string is not a primitive type

            ReflectionExtensions.ReflectionEnvironment.AddPrimitive<string>();  //we add string to our primitive type list
            Assert.IsTrue(typeof(string).IsPrimitive(useStaticPrimitiveList: true)); //string is NOW a primitive type

            Assert.IsTrue(typeof(string).IsPrimitive(useStaticPrimitiveList: false, customPrimitiveTypes: new List<Type>() //string is NOW a primitive type because we added to a custom and temp list
            {
                typeof(string)
            }));

            Assert.IsTrue(typeof(string).IsPrimitive(useStaticPrimitiveList: true, customPrimitiveTypes: new List<Type>() //string is NOW a primitive type because we added to a custom and temp list and it is on primitiveList previously added
            {
                typeof(string)
            }));
        }
    }
}
