using System;
using System.Collections.Generic;
using System.Linq;
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
            Assert.IsTrue(typeof(int).IsPrimitive()); //Int is primitive type.

            Assert.IsFalse(typeof(string).IsPrimitive()); //string is not a primitive type
            Assert.IsFalse(typeof(string).IsPrimitive(useStaticPrimitiveList: true)); //string is not a primitive type

            ReflectionExtensions.ReflectionEnvironment
                .AddPrimitive<string>(); //we add string to our primitive type list
            Assert.IsTrue(typeof(string).IsPrimitive(useStaticPrimitiveList: true)); //string is NOW a primitive type

            Assert.IsTrue(typeof(string).IsPrimitive(useStaticPrimitiveList: false,
                customPrimitiveTypes: new
                    List<Type>() //string is NOW a primitive type because we added to a custom and temp list
                    {
                        typeof(string)
                    }));

            Assert.IsTrue(typeof(string).IsPrimitive(useStaticPrimitiveList: true,
                customPrimitiveTypes: new
                    List<Type>() //string is NOW a primitive type because we added to a custom and temp list and it is on primitiveList previously added
                    {
                        typeof(string)
                    }));

            ReflectionExtensions.ReflectionEnvironment
                .RemovePrimitive<string>(); //we remove string to our primitive type list
            Assert.IsFalse(typeof(string).IsPrimitive(useStaticPrimitiveList: true)); //string is not a primitive type again
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),AllowDerivedTypes = true)]
        public void GetChildrenValueMethodKoTest()
        {
            var patrol = new Patrol()
            {
                Dog = new Dog()
                {
                    Name = "DogName"
                },
                Human = new Human()
                {
                    Name = "HumanName",
                    Clothes = new Cloth()
                    {
                        Description = "HumanCloth"
                    }
                }
            };
           
            var clothesProperty = typeof(Human).GetProperty("Clothes");
            var humanClothes = clothesProperty?.GetValue(patrol);
            Assert.IsNull(humanClothes);
        }

        [TestMethod]
        public void GetFilteredPropertiesOk()
        {
            Assert.AreEqual(1,typeof(Human).GetFilteredProperties(info => info.Name.Equals(nameof(Human.Name))).Count());
        }

        [TestMethod]
        public void ContainsPropertyOk()
        {
            Assert.IsTrue(typeof(Human).ContainsProperty(typeof(Human).GetProperty(nameof(Human.Name))));
        }

        [TestMethod]
        public void GetChildrenValueMethodOkTest()
        {
            var patrol = new Patrol()
            {
                Dog = new Dog()
                {
                    Name = "DogName"
                },
                Human = new Human()
                {
                    Name = "HumanName",
                    Clothes = new Cloth()
                    {
                        Description = "HumanCloth"
                    }
                }
            };
           
            var clothesProperty = typeof(Human).GetProperty("Clothes");
            var humanClothes = clothesProperty?.GetChildrenValue(patrol);
            Assert.AreEqual("HumanCloth",((Cloth)humanClothes)?.Description);
        }
    }
}
