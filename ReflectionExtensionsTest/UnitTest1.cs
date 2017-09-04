using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReflectionExtensionsPack;

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
    }

    public class Dog : Animal
    {
        
    }

    public class Animal : IKillable
    {
        public void Die()
        {
            Console.WriteLine("I'm dead :(");
        }
    }

    public interface IKillable
    {
        void Die();
    }
}
