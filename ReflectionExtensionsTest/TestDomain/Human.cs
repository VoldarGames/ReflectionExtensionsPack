using System;
using ReflectionExtensionsTest.TestDomain.Interfaces;

namespace ReflectionExtensionsTest.TestDomain
{
    public class Human : IKillable
    {
        public string Name { get; set; }
        public Cloth Clothes { get; set; }
        public void Die()
        {
            Console.WriteLine("I'm a dead human :(");
        }
    }
}