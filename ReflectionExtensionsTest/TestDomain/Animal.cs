using System;
using ReflectionExtensionsTest.TestDomain.Interfaces;

namespace ReflectionExtensionsTest.TestDomain
{
    public class Animal : IKillable
    {
        public void Die()
        {
            Console.WriteLine("I'm dead :(");
        }
    }
}